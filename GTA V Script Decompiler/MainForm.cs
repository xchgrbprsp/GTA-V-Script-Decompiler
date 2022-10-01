using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Decompiler
{

	public partial class MainForm : Form
	{
		string filename = "";
		ScriptFile OpenFile;
		Style Highlight;
		Queue<string> CompileList;
		string SaveDirectory;
		readonly List<Disassembly> DisassembyWindows = new();

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
			Highlight =  new TextStyle(Brushes.Black, Brushes.Orange, fctb1.DefaultStyle.FontStyle);

		}

		void UpdateStatus(string text)
		{
			toolStripStatusLabel1.Text = text;
			Application.DoEvents();
		}

		void ResetLoadedFile()
		{
            fctb1.Clear();
            listView1.Items.Clear();

            foreach (var dis in DisassembyWindows)
                dis.Dispose();

            DisassembyWindows.Clear();

			OpenFile = null;
            GC.Collect();
        }

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "GTA V Script Files|*.ysc;*.ysc.full";

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				DateTime Start = DateTime.Now;
				filename = Path.GetFileNameWithoutExtension(ofd.FileName);

				ResetLoadedFile();

				UpdateStatus("Opening script file...");
				string ext = Path.GetExtension(ofd.FileName);
				if (ext == ".full") // Handle openIV exporting pc scripts as *.ysc.full
				{
					ext = Path.GetExtension(Path.GetFileNameWithoutExtension(ofd.FileName));
				}

                OpenFile = new ScriptFile(ofd.OpenFile());

				UpdateStatus("Decompiled script file. Time taken: " + (DateTime.Now - Start).ToString());
				MemoryStream ms = new MemoryStream();

				OpenFile.Save(ms, false);

				foreach (KeyValuePair<string, Tuple<int, int>> locations in OpenFile.Function_loc)
				{
					listView1.Items.Add(new ListViewItem(new string[] {locations.Key, locations.Value.Item1.ToString(), locations.Value.Item2.ToString()}));
				}

				OpenFile.Close();

				StreamReader sr = new StreamReader(ms);
				ms.Position = 0;

				UpdateStatus("Loading text in viewer...");

				fctb1.Text = sr.ReadToEnd();
				SetFileName(filename);
				UpdateStatus("Ready. Time taken: " + (DateTime.Now - Start).ToString());
			}
		}

		private async void directoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
            CommonOpenFileDialog fsd = new CommonOpenFileDialog();
            fsd.IsFolderPicker = true;
			if (fsd.ShowDialog() == CommonFileDialogResult.Ok)
			{
				await BatchDecompile(fsd.FileName);
			}
        }

		private async Task BatchDecompile(string dirPath)
		{
            CompileList = new Queue<string>();
			var tasks = new List<Task>();

            DateTime Start = DateTime.Now;
            SaveDirectory = Path.Combine(dirPath, "exported");
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);

            foreach (string file in Directory.GetFiles(dirPath, "*.ysc"))
            {
                CompileList.Enqueue(file);
            }

            foreach (string file in Directory.GetFiles(dirPath, "*.ysc.full"))
            {
                CompileList.Enqueue(file);
            }

			Enabled = false;

            var progressBar = new ProgressBar("Directory Export", 1, CompileList.Count);
            progressBar.Show();

            if (Properties.Settings.Default.UseMultithreading)
            {
                for (int i = 0; i < Environment.ProcessorCount; i++)
                {
					tasks.Add(Decompile(progressBar));
                }

				await Task.WhenAll(tasks);
            }
            else
            {
                await Decompile(progressBar);
            }

			Enabled = true;
            UpdateStatus("Directory Extracted, Time taken: " + (DateTime.Now - Start).ToString());
        }

		private async Task Decompile(ProgressBar progressBar)
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
					await Task.Run(() =>
					{
						ScriptFile scriptFile = new ScriptFile(File.OpenRead(scriptToDecode));
						scriptFile.Save(Path.Combine(SaveDirectory, Path.GetFileNameWithoutExtension(scriptToDecode) + ".c"));
						scriptFile.Close();
					});
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error decompiling script " + Path.GetFileNameWithoutExtension(scriptToDecode) + " - " + ex.Message);
				}

                progressBar.IncrementValue();
            }
		}

		private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "GTA V Script Files|*.ysc;*.ysc.full";

			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				DateTime Start = DateTime.Now;
				ScriptFile file = new ScriptFile(ofd.OpenFile());
				file.Save(Path.Combine(Path.GetDirectoryName(ofd.FileName),
				Path.GetFileNameWithoutExtension(ofd.FileName) + ".c"));
				file.Close();
				UpdateStatus("File Saved, Time taken: " + (DateTime.Now - Start).ToString());
			}
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
			UpdateStatus("Expanding all blocks...");
			fctb1.ExpandAllFoldingBlocks();
            UpdateStatus("Ready");
        }

		private void collaspeAllBlocksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateStatus("Collasping all blocks...");
			fctb1.CollapseAllFoldingBlocks();
            UpdateStatus("Ready");
        }

		private void fctb1_MouseClick(object sender, MouseEventArgs e)
		{
			opening = false;
			forceclose = true;
			timer2.Start();
			timer1.Stop();
			if (e.Button == MouseButtons.Right)
			{
				if (fctb1.SelectionLength == 0)
				{
					fctb1.SelectionStart = fctb1.PointToPosition(fctb1.PointToClient(Cursor.Position));
				}
				cmsText.Show();
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
			sfd.Filter = "Decompiled Script Files|*.c;*.c4;*.sc;*.sch\"";
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

		private void stringsTableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Title = "Select location to save string table";
			sfd.Filter = "Text files|*.txt|All Files|*.*";
			sfd.FileName = ((filename.Contains('.')) ? filename.Remove(filename.IndexOf('.')) : filename) + "(Strings).txt";
			if (sfd.ShowDialog() != DialogResult.OK)
				return;
			StreamWriter sw = File.CreateText(sfd.FileName);
			foreach (string line in OpenFile.GetStringTable())
			{
				sw.WriteLine(line);
			}
			sw.Close();
			MessageBox.Show("File Saved");
		}

		private void nativeTableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Title = "Select location to save native table";
			sfd.Filter = "Text files|*.txt|All Files|*.*";
			sfd.FileName = ((filename.Contains('.')) ? filename.Remove(filename.IndexOf('.')) : filename) + "(natives).txt";
			if (sfd.ShowDialog() != DialogResult.OK)
				return;
			StreamWriter sw = File.CreateText(sfd.FileName);
			foreach (string line in OpenFile.GetNativeTable())
			{
				sw.WriteLine(line);
			}
			sw.Close();
			MessageBox.Show("File Saved");
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
	}
}
