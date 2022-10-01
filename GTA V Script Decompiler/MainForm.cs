using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Decompiler
{

	public partial class MainForm : Form
	{
		bool loadingfile = false;
		string filename = "";
		private bool scriptopen = false;
		ScriptFile OpenFile;
		Style highlight;
		Queue<string> CompileList;
		List<Tuple<uint, string>> FoundStrings;
		uint[] HashToFind;
		string SaveDirectory;
		List<Disassembly> DisassembyWindows = new();

		public bool ScriptOpen
		{
			get { return scriptopen; }
			set { extractToolStripMenuItem.Visible = extractToolStripMenuItem.Enabled = scriptopen = value; }
		}


		public MainForm()
		{
			InitializeComponent();

			panel1.Size = new Size(0, panel1.Height);
			
			showArraySizeToolStripMenuItem.Checked = Properties.Settings.Default.ShowArraySize;
			reverseHashesToolStripMenuItem.Checked = Properties.Settings.Default.ReverseHashes;
			showLocalizedTextsToolStripMenuItem.Checked = Properties.Settings.Default.ShowLocalizedTexts;
            declareVariablesToolStripMenuItem.Checked = Properties.Settings.Default.DeclareVariables;
			shiftVariablesToolStripMenuItem.Checked = Properties.Settings.Default.ShiftVariables;
			showFuncPointerToolStripMenuItem.Checked = Properties.Settings.Default.ShowFunctionPointers;
			useMultiThreadingToolStripMenuItem.Checked = Properties.Settings.Default.UseMultithreading;
			includeFunctionPositionToolStripMenuItem.Checked = Properties.Settings.Default.IncludeFunctionPosition;
			includeFunctionHashToolStripMenuItem.Checked = Properties.Settings.Default.IncludeFunctionHash;
            includeNativeNamespaceToolStripMenuItem.Checked = Properties.Settings.Default.ShowNativeNamespace;
			globalAndStructHexIndexingToolStripMenuItem.Checked = Properties.Settings.Default.HexIndex;
			uppercaseNativesToolStripMenuItem.Checked = Properties.Settings.Default.UppercaseNatives;

			showLineNumbersToolStripMenuItem.Checked = fctb1.ShowLineNumbers = Properties.Settings.Default.LineNumbers;

            ToolStripMenuItem t = null;
			switch (Program.Find_getINTType())
			{
				case Program.IntType._int:
					t = intToolStripMenuItem;
					break;
				case Program.IntType._uint:
					t = uintToolStripMenuItem;
					break;
				case Program.IntType._hex:
					t = hexToolStripMenuItem;
					break;
			}
			t.Checked = true;
			t.Enabled = false;
			highlight =  new TextStyle(Brushes.Black, Brushes.Orange, fctb1.DefaultStyle.FontStyle);

		}

		void updatestatus(string text)
		{
			toolStripStatusLabel1.Text = text;
			Application.DoEvents();
		}

		void ready()
		{
			updatestatus("Ready");
			loadingfile = false;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "GTA V Script Files|*.ysc;*.ysc.full";
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				DateTime Start = DateTime.Now;
				filename = Path.GetFileNameWithoutExtension(ofd.FileName);
				loadingfile = true;
				fctb1.Clear();
				listView1.Items.Clear();
				
				foreach (var dis in DisassembyWindows)
					dis.Dispose();

				DisassembyWindows.Clear();

				updatestatus("Opening Script File...");
				string ext = Path.GetExtension(ofd.FileName);
				if (ext == ".full") //handle openIV exporting pc scripts as *.ysc.full
				{
					ext = Path.GetExtension(Path.GetFileNameWithoutExtension(ofd.FileName));
				}
#if !DEBUG
				try
				{
#endif
                OpenFile = new ScriptFile(ofd.OpenFile());
				GC.Collect();
#if !DEBUG
				}
				catch (Exception ex)
				{
					updatestatus("Error decompiling script " + ex.Message);
					return;
				}
#endif
				updatestatus("Decompiled Script File, Time taken: " + (DateTime.Now - Start).ToString());
				MemoryStream ms = new MemoryStream();

				OpenFile.Save(ms, false);


				foreach (KeyValuePair<string, Tuple<int, int>> locations in OpenFile.Function_loc)
				{
					listView1.Items.Add(new ListViewItem(new string[] {locations.Key, locations.Value.Item1.ToString(), locations.Value.Item2.ToString()}));
				}
				OpenFile.Close();
				StreamReader sr = new StreamReader(ms);
				ms.Position = 0;
				updatestatus("Loading Text in Viewer...");
				fctb1.Text = sr.ReadToEnd();
				SetFileName(filename);
				ScriptOpen = true;
				updatestatus("Ready, Time taken: " + (DateTime.Now - Start).ToString());
				//

			}
		}

		private void directoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CompileList = new Queue<string>();
			Program.ThreadCount = 0;
            CommonOpenFileDialog fsd = new CommonOpenFileDialog();
			fsd.IsFolderPicker = true;
			if (fsd.ShowDialog() == CommonFileDialogResult.Ok)
			{
				DateTime Start = DateTime.Now;
				SaveDirectory = Path.Combine(fsd.FileName, "exported");
				if (!Directory.Exists(SaveDirectory))
					Directory.CreateDirectory(SaveDirectory);
				this.Hide();

				foreach (string file in Directory.GetFiles(fsd.FileName, "*.ysc"))
				{
					CompileList.Enqueue(file);
				}
				foreach (string file in Directory.GetFiles(fsd.FileName, "*.ysc.full"))
				{
					CompileList.Enqueue(file);
				}
				if (Properties.Settings.Default.UseMultithreading)
				{
					for (int i = 0; i < Environment.ProcessorCount - 1; i++)
					{
						Program.ThreadCount++;
						new Thread(Decompile, 10000000).Start();
					}
					Program.ThreadCount++;
					Decompile();
					while (Program.ThreadCount > 0)
					{
						Thread.Sleep(10);
					}
				}
				else
				{
					Program.ThreadCount++;
					Decompile();
				}

				updatestatus("Directory Extracted, Time taken: " + (DateTime.Now - Start).ToString());
			}
			this.Show();
		}

		private void Decompile()
		{
			while (CompileList.Count > 0)
			{
				string scriptToDecode;
				lock (Program.ThreadLock)
				{
					scriptToDecode = CompileList.Dequeue();
				}
				try
				{
					ScriptFile scriptFile = new ScriptFile(File.OpenRead(scriptToDecode));
					scriptFile.Save(Path.Combine(SaveDirectory, Path.GetFileNameWithoutExtension(scriptToDecode) + ".c"));
					scriptFile.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error decompiling script " + Path.GetFileNameWithoutExtension(scriptToDecode) + " - " + ex.Message);
				}
			}
			Program.ThreadCount--;
		}

		private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "GTA V Script Files|*.xsc;*.csc;*.ysc";
#if !DEBUG
			try
			{
#endif
				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{

					DateTime Start = DateTime.Now;
					ScriptFile file = new ScriptFile(ofd.OpenFile());
					file.Save(Path.Combine(Path.GetDirectoryName(ofd.FileName),
						Path.GetFileNameWithoutExtension(ofd.FileName) + ".c"));
					file.Close();
					updatestatus("File Saved, Time taken: " + (DateTime.Now - Start).ToString());
				}
#if !DEBUG
			}
			catch (Exception ex)
			{
				updatestatus("Error decompiling script " + ex.Message);
			}
#endif
		}

		#region Config Options

		private void intstylechanged(object sender, EventArgs e)
		{
			ToolStripMenuItem clicked = (ToolStripMenuItem) sender;
			foreach (ToolStripMenuItem t in intStyleToolStripMenuItem.DropDownItems)
			{
				t.Enabled = true;
				t.Checked = false;
			}
			clicked.Checked = true;
			clicked.Enabled = false;
			Properties.Settings.Default.IntStyle = clicked.Text.ToLower();
			Program.Find_getINTType();
		}

		private void showArraySizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showArraySizeToolStripMenuItem.Checked = !showArraySizeToolStripMenuItem.Checked;
			Properties.Settings.Default.ShowArraySize = showArraySizeToolStripMenuItem.Checked;
			Properties.Settings.Default.Save();
        }

		private void reverseHashesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			reverseHashesToolStripMenuItem.Checked = !reverseHashesToolStripMenuItem.Checked;
            Properties.Settings.Default.ReverseHashes = reverseHashesToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void showLocalizedTextsStripMenuItem_Click(object sender, EventArgs e)
        {
            showLocalizedTextsToolStripMenuItem.Checked = !showLocalizedTextsToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowLocalizedTexts = showLocalizedTextsToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void declareVariablesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			declareVariablesToolStripMenuItem.Checked = !declareVariablesToolStripMenuItem.Checked;
            Properties.Settings.Default.DeclareVariables = declareVariablesToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void shiftVariablesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			shiftVariablesToolStripMenuItem.Checked = !shiftVariablesToolStripMenuItem.Checked;
			Properties.Settings.Default.ShiftVariables = shiftVariablesToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void showLineNumbersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showLineNumbersToolStripMenuItem.Checked = !showLineNumbersToolStripMenuItem.Checked;
            Properties.Settings.Default.LineNumbers = showLineNumbersToolStripMenuItem.Checked;
			fctb1.ShowLineNumbers = showLineNumbersToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void showFuncPointerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showFuncPointerToolStripMenuItem.Checked = !showFuncPointerToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowFunctionPointers = showFuncPointerToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

		private void RebuildNativeFiles()
		{
			if (Program.x64nativefile != null)
			{
				Program.x64nativefile = new x64NativeFile();
			}
		}

		private void includeNativeNamespaceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			includeNativeNamespaceToolStripMenuItem.Checked = !includeNativeNamespaceToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowNativeNamespace = includeNativeNamespaceToolStripMenuItem.Checked;
			RebuildNativeFiles();
			Properties.Settings.Default.Save();
        }

		private void globalAndStructHexIndexingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			globalAndStructHexIndexingToolStripMenuItem.Checked = !globalAndStructHexIndexingToolStripMenuItem.Checked;
			Properties.Settings.Default.HexIndex = globalAndStructHexIndexingToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

		private void useMultiThreadingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			useMultiThreadingToolStripMenuItem.Checked = !useMultiThreadingToolStripMenuItem.Checked;
			Properties.Settings.Default.UseMultithreading = useMultiThreadingToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

		private void includeFunctionPositionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			includeFunctionPositionToolStripMenuItem.Checked = !includeFunctionPositionToolStripMenuItem.Checked;
			Properties.Settings.Default.IncludeFunctionPosition = includeFunctionPositionToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void includeFunctionHashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            includeFunctionHashToolStripMenuItem.Checked = !includeFunctionHashToolStripMenuItem.Checked;
            Properties.Settings.Default.IncludeFunctionHash = includeFunctionHashToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void uppercaseNativesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			uppercaseNativesToolStripMenuItem.Checked = !uppercaseNativesToolStripMenuItem.Checked;
			Properties.Settings.Default.UppercaseNatives = uppercaseNativesToolStripMenuItem.Checked;
			RebuildNativeFiles();
            Properties.Settings.Default.Save();
        }

		#endregion

		#region Function Location

		bool opening = false;
		bool forceclose = false;

		private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listView1.SelectedItems.Count == 1)
			{
				int num = Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text);
				fctb1.Selection = new FastColoredTextBoxNS.Range(fctb1, 0, num, 0, num);
				fctb1.DoSelectionVisible();
			}
		}

		private void listView1_MouseLeave(object sender, EventArgs e)
		{
			timer1.Start();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (panel1.ClientRectangle.Contains(panel1.PointToClient(Control.MousePosition)))
			{
				return;
			}
			opening = false;
			timer2.Start();
			timer1.Stop();
		}


		private void listView1_MouseEnter(object sender, EventArgs e)
		{
			if (forceclose)
				return;
			timer1.Stop();
			opening = true;
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			if (opening)
			{
				if (panel1.Size.Width < 165) panel1.Size = new Size(panel1.Size.Width + 6, panel1.Size.Height);
				else
				{
					panel1.Size = new Size(170, panel1.Size.Height);
					timer2.Stop();
					forceclose = false;
				}

			}
			if (!opening)
			{
				if (panel1.Size.Width > 2) panel1.Size = new Size(panel1.Size.Width - 2, panel1.Size.Height);
				else
				{
					panel1.Size = new Size(0, panel1.Size.Height);
					timer2.Stop();
					forceclose = false;
				}

			}

		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			opening = !opening;
			if (!opening)
				forceclose = true;
			timer2.Start();
			columnHeader1.Width = 80;
			columnHeader2.Width = 76;
		}

        #endregion

        private void resetGlobalTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.globalTypeMgr.Reset();
        }

        private void entitiesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScriptFile.HashBank.Export_Entities();
		}

		private void nativesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
				"natives_exp.dat");
			FileStream fs = File.Create(path);
			new MemoryStream(Properties.Resources.natives).CopyTo(fs);
			fs.Close();
			System.Diagnostics.Process.Start(
				Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)), "natives_exp.dat");
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void expandAllBlocksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			updatestatus("Expanding all blocks...");
			fctb1.ExpandAllFoldingBlocks();
			ready();
		}

		private void collaspeAllBlocksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			updatestatus("Collasping all blocks...");
			fctb1.CollapseAllFoldingBlocks();
			ready();
		}

		private void fctb1_MouseClick(object sender, MouseEventArgs e)
		{
			opening = false;
			forceclose = true;
			timer2.Start();
			timer1.Stop();
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (fctb1.SelectionLength == 0)
				{
					fctb1.SelectionStart = fctb1.PointToPosition(fctb1.PointToClient(Cursor.Position));
				}
				cmsText.Show();
			}
		}

		public string getfunctionfromline(int line)
		{
			if (listView1.Items.Count == 0)
				return "";

			int temp;
			if (int.TryParse(listView1.Items[0].SubItems[1].Text, out temp))
			{
				if (line < temp - 1)
					return "Local Vars";
			}
			else return "";
			int max = listView1.Items.Count - 1;
			for (int i = 0; i < max; i++)
			{
				if (!int.TryParse(listView1.Items[i].SubItems[1].Text, out temp))
					continue;
				if (line >= temp)
				{
					if (!int.TryParse(listView1.Items[i + 1].SubItems[1].Text, out temp))
						continue;
					if (line < temp - 1)
					{
						return listView1.Items[i].SubItems[0].Text;
					}
				}
			}
			if (int.TryParse(listView1.Items[max].SubItems[1].Text, out temp))
			{
				if (line >= temp)
					return listView1.Items[max].SubItems[0].Text;
			}
			return "";
		}

		private void fctb1_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				toolStripStatusLabel3.Text = getfunctionfromline(fctb1.Selection.Start.iLine + 1);
				fctb1.Range.ClearStyle(highlight);
				if (fctb1.SelectionLength > 0)
				{
					if (!fctb1.SelectedText.Contains('\n') && !fctb1.SelectedText.Contains('\n'))
						fctb1.Range.SetStyle(highlight, "\\b" + fctb1.Selection.Text + "\\b", RegexOptions.IgnoreCase);
				}
				GetContextItems();
			}
			catch
			{
			}
		}

		public void fill_function_table()
		{
			try
			{
				loadingfile = true;
				Dictionary<string, int> functionloc = new Dictionary<string, int>();
				for (int i = 0; i < fctb1.LinesCount; i++)
				{
					if (fctb1.Lines[i].Length == 0)
						continue;
					if (fctb1.Lines[i].Contains(' '))
					{
						if (!fctb1.Lines[i].Contains('('))
							continue;
						string type = fctb1.Lines[i].Remove(fctb1.Lines[i].IndexOf(' '));
						switch (type.ToLower())
						{
							case "void":
							case "var":
							case "float":
							case "bool":
							case "int":
							case "vector3":
							case "*string":
								string name = fctb1.Lines[i].Remove(fctb1.Lines[i].IndexOf('(')).Substring(fctb1.Lines[i].IndexOf(' ') + 1);
								functionloc.Add(name, i + 1);
								continue;
							default:
								if (type.ToLower().StartsWith("struct<"))
									goto case "var";
								break;

						}
					}
				}
				listView1.Items.Clear();
				foreach (KeyValuePair<string, int> locations in functionloc)
				{
					listView1.Items.Add(new ListViewItem(new string[] {locations.Key, locations.Value.ToString()}));
				}
				loadingfile = false;
			}
			catch
			{
				loadingfile = false;
			}
		}

		private void timer3_Tick(object sender, EventArgs e)
		{
			timer3.Stop();
			fill_function_table();
		}

		private void fctb1_LineInserted(object sender, FastColoredTextBoxNS.LineInsertedEventArgs e)
		{
			if (!loadingfile)
				timer3.Start();
		}

		private void fctb1_LineRemoved(object sender, FastColoredTextBoxNS.LineRemovedEventArgs e)
		{
			if (!loadingfile)
				timer3.Start();
		}

		private void openCFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "C Source files *.c|*.c";
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				loadingfile = true;
				filename = Path.GetFileNameWithoutExtension(ofd.FileName);
				fctb1.Clear();
				listView1.Items.Clear();
				updatestatus("Loading Text in Viewer...");
				fctb1.OpenFile(ofd.FileName);
				updatestatus("Loading Functions...");
				fill_function_table();
				SetFileName(filename);
				ready();
				ScriptOpen = false;
			}
		}

		private void SetFileName(string name)
		{
			if (name == null)
				this.Text = "GTA V High Level Decompiler";
			if (name.Length == 0)
				this.Text = "GTA V High Level Decompiler";
			else
			{
				if (name.Contains('.'))
					name = name.Remove(name.IndexOf('.'));
				this.Text = "GTA V High Level Decompiler - " + name;
			}
		}

		private void saveCFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "C Source files *.c|*.c";
			sfd.FileName = filename + ".c";
			if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				fctb1.SaveToFile(sfd.FileName, System.Text.Encoding.Default);
				MessageBox.Show("File Saved");
			}
		}

		private void cmsText_Opening(object sender, CancelEventArgs e)
		{
			GetContextItems();
			if (cmsText.Items.Count == 0) e.Cancel = true;
		}

		bool islegalchar(char c)
		{
			if (char.IsLetterOrDigit(c)) return true;
			return c == '_';

		}

		string GetWordAtCursor()
		{
			string line = fctb1.Lines[fctb1.Selection.Start.iLine];
			if (line.Length == 0)
				return "";
			int pos = fctb1.Selection.Start.iChar;
			if (pos == line.Length)
				return "";
			int min = pos, max = pos;
			while (min > 0)
			{
				if (islegalchar(line[min - 1]))
					min--;
				else
					break;
			}
			int len = line.Length;
			while (max < len)
			{
				if (islegalchar(line[max]))
					max++;
				else
					break;
			}
			return line.Substring(min, max - min);
		}

		private void GetContextItems()
		{
			string word = GetWordAtCursor();
			cmsText.Items.Clear();
			foreach (ListViewItem lvi in listView1.Items)
			{
				if (lvi.Text == word)
				{
					cmsText.Items.Add(new ToolStripMenuItem("Goto Declaration (" + lvi.Text + ")", null,
						new EventHandler(delegate(object o, EventArgs a)
						{
							int num = Convert.ToInt32(lvi.SubItems[1].Text);
							fctb1.Selection = new FastColoredTextBoxNS.Range(fctb1, 0, num, 0, num);
							fctb1.DoSelectionVisible();
						}), Keys.F12));

                    cmsText.Items.Add(new ToolStripMenuItem("Disassemble (" + lvi.Text + ")", null,
                        new EventHandler(delegate (object o, EventArgs a)
                        {
							foreach (var func in OpenFile.Functions)
							{
								if (func.Name == word)
								{
									var dis = new Disassembly(func);
									dis.Show();
									DisassembyWindows.Add(dis);
                                    break;
								}
							}
                        }), Keys.F10));
                }
			}

		}

		private void fullNativeInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
		}

		private void fullPCNativeInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// ScriptFile.X64npi.exportnativeinfo();
			updatestatus("Not implemented");
		}


		private void stringsTableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ScriptOpen)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Title = "Select location to save string table";
				sfd.Filter = "Text files|*.txt|All Files|*.*";
				sfd.FileName = ((filename.Contains('.')) ? filename.Remove(filename.IndexOf('.')) : filename) + "(Strings).txt";
				if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					return;
				StreamWriter sw = File.CreateText(sfd.FileName);
				foreach (string line in OpenFile.GetStringTable())
				{
					sw.WriteLine(line);
				}
				sw.Close();
				MessageBox.Show("File Saved");
			}
			else
			{
				MessageBox.Show("No script file is open");
			}

		}

		private void nativeTableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ScriptOpen)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Title = "Select location to save native table";
				sfd.Filter = "Text files|*.txt|All Files|*.*";
				sfd.FileName = ((filename.Contains('.')) ? filename.Remove(filename.IndexOf('.')) : filename) + "(natives).txt";
				if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					return;
				StreamWriter sw = File.CreateText(sfd.FileName);
				foreach (string line in OpenFile.GetNativeTable())
				{
					sw.WriteLine(line);
				}
				sw.Close();
				MessageBox.Show("File Saved");
			}
			else
			{
				MessageBox.Show("No script file is open");
			}
		}

		/// <summary>
		/// Generates a c like header for all the natives. made it back when i was first trying to recompile the files back to *.*sc
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nativehFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ScriptOpen)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Title = "Select location to save native table";
				sfd.Filter = "C header files|*.h|All Files|*.*";
				sfd.FileName = ((filename.Contains('.')) ? filename.Remove(filename.IndexOf('.')) : filename) + ".h";
				if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					return;
				StreamWriter sw = File.CreateText(sfd.FileName);
				sw.WriteLine("/*************************************************************");
				sw.WriteLine("******* Header file generated for " + filename + " *******");
				sw.WriteLine("*************************************************************/\n");
				sw.WriteLine(
					"#region Vectors\ntypedef struct Vector3{\n\tfloat x;\n\tfloat y;\n\tfloat z;\n} Vector3, *PVector3;\n\n");
				sw.WriteLine("extern Vector3 VectorAdd(Vector3 v0, Vector3 v1);");
				sw.WriteLine("extern Vector3 VectorSub(Vector3 v0, Vector3 v1);");
				sw.WriteLine("extern Vector3 VectorMult(Vector3 v0, Vector3 v1);");
				sw.WriteLine("extern Vector3 VectorDiv(Vector3 v0, Vector3 v1);");
				sw.WriteLine("extern Vector3 VectorNeg(Vector3 v0);\n#endregion\n\n");
				sw.WriteLine("#define TRUE 1\n#define FALSE 0\n#define true 1\n#define false 0\n");
				sw.WriteLine("typedef unsigned int uint;");
				sw.WriteLine("typedef uint bool;");
				sw.WriteLine("typedef uint var;");
				sw.WriteLine("");
				foreach (string line in OpenFile.GetNativeHeader())
				{
					sw.WriteLine("extern " + line);
				}
				sw.Close();
				MessageBox.Show("File Saved");
			}
			else
			{
				MessageBox.Show("No script file is open");
			}
		}

		private void navigateForwardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!fctb1.NavigateForward())
			{
				MessageBox.Show("Error, cannont navigate forwards anymore");
			}
		}

		private void navigateBackwardsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!fctb1.NavigateBackward())
			{
				MessageBox.Show("Error, cannont navigate backwards anymore");
			}
		}


		/// <summary>
		/// The games language files store items as hashes. This function will grab all strings in a all scripts in a directory
		/// and hash each string and them compare with a list of hashes supplied in the input box. Any matches get saved to a file STRINGS.txt in the directory
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void findHashFromStringsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			InputBox IB = new InputBox();
			if (!IB.ShowList("Input Hash", "Input hash to find", this))
				return;
			uint hash;
			List<uint> Hashes = new List<uint>();
			foreach (string result in IB.ListValue)
			{

				if (result.StartsWith("0x"))
				{
					if (uint.TryParse(result.Substring(2), System.Globalization.NumberStyles.HexNumber,
						new System.Globalization.CultureInfo("en-gb"), out hash))
					{
						Hashes.Add(hash);
					}
					else
					{
						MessageBox.Show($"Error converting {result} to hash value");
					}
				}
				else
				{
					if (uint.TryParse(result, out hash))
					{
						Hashes.Add(hash);
					}
					else
					{
						MessageBox.Show($"Error converting {result} to hash value");
					}
				}
			}
			if (Hashes.Count == 0)
			{
				MessageBox.Show($"Error, no hashes inputted, please try again");
				return;
			}
			HashToFind = Hashes.ToArray();
			CompileList = new Queue<string>();
			FoundStrings = new List<Tuple<uint, string>>();
			Program.ThreadCount = 0;
            CommonOpenFileDialog fsd = new CommonOpenFileDialog();
			if (fsd.ShowDialog() == CommonFileDialogResult.Ok)
			{
				DateTime Start = DateTime.Now;
				this.Hide();

				foreach (string file in Directory.GetFiles(fsd.FileName, "*.ysc"))
				{
					CompileList.Enqueue(file);
				}
				foreach (string file in Directory.GetFiles(fsd.FileName, "*.ysc.full"))
				{
                    CompileList.Enqueue(file);
                }
                if (Properties.Settings.Default.UseMultithreading)
				{
					for (int i = 0; i < Environment.ProcessorCount - 1; i++)
					{
						Program.ThreadCount++;
						new Thread(FindString, 10000000).Start();
						Thread.Sleep(0);
					}
					Program.ThreadCount++;
					FindString();
					while (Program.ThreadCount > 0)
					{
						Thread.Sleep(10);
					}
				}
				else
				{
					Program.ThreadCount++;
					FindString();
				}

				if (FoundStrings.Count == 0)
					updatestatus($"No Strings Found, Time taken: {DateTime.Now - Start}");
				else
				{
					updatestatus($"Found {FoundStrings.Count} strings, Time taken: {DateTime.Now - Start}");
					FoundStrings.Sort((x, y) => x.Item1.CompareTo(y.Item1));
					using (StreamWriter oFile = File.CreateText(Path.Combine(fsd.FileName, "STRINGS.txt")))
					{
						foreach (Tuple<uint, string> Item in FoundStrings)
						{
							oFile.WriteLine($"0x{Utils.FormatHexHash(Item.Item1)} : \"{Item.Item2}\"");
						}
					}
				}
			}
			this.Show();
		}

		/// <summary>
		/// This does the actual searching of the hashes from the above function. Designed to run on multiple threads
		/// </summary>
		private void FindString()
		{
			while (CompileList.Count > 0)
			{
				string scriptToSearch;
				lock (Program.ThreadLock)
				{
					scriptToSearch = CompileList.Dequeue();
				}
				using (Stream ScriptFile = File.OpenRead(scriptToSearch))
				{
					ScriptHeader header = ScriptHeader.Generate(ScriptFile);
					StringTable table = new StringTable(ScriptFile, header.StringTableOffsets, header.StringBlocks, header.StringsSize);
					foreach (string str in table.Values)
					{
						if (HashToFind.Contains(Utils.Joaat(str)))
						{
							if (IsLower(str))
								continue;
							lock (Program.ThreadLock)
							{
								if (!FoundStrings.Any(item => item.Item2 == str))
								{
									FoundStrings.Add(new Tuple<uint, string>(Utils.Joaat(str), str));
								}
							}
						}
					}
				}
			}
			Program.ThreadCount--;
		}

		private static bool IsLower(string s)
		{
			foreach (char c in s)
				if (char.IsLower(c))
				{
					return true;
				}
			return false;
		}
	}
}
