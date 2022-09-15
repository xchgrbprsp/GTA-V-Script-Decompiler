using System;
using System.IO;
using System.Windows.Forms;

namespace Decompiler
{
	static class Program
	{
		public static x64NativeFile x64nativefile;
		public static Object ThreadLock;
		public static int ThreadCount;
		public static NativeDB nativeDB;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			ThreadLock = new object();

			x64nativefile = new x64NativeFile();

			ScriptFile.HashBank = new Hashes();

			nativeDB = new NativeDB();
			nativeDB.LoadData();

			if (args.Length == 0)
			{
				Application.EnableVisualStyles();
				Application.SetHighDpiMode(HighDpiMode.SystemAware);
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
			else
			{
				DateTime Start = DateTime.Now;
				string ext = Path.GetExtension(args[0]);
				if (ext == ".full")
				{
					ext = Path.GetExtension(Path.GetFileNameWithoutExtension(args[0]));
				}
				ScriptFile fileopen;
				Console.WriteLine("Decompiling " + args[0] + "...");
				try
				{
					fileopen = new ScriptFile(File.OpenRead(args[0]));
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error decompiling script " + ex.Message);
					return;
				}
				Console.WriteLine("Decompiled in " + (DateTime.Now - Start).ToString());
				fileopen.Save(File.OpenWrite(args[0] + ".c"), true);
				Console.WriteLine("Extracing native table...");
				StreamWriter fw = new StreamWriter(File.OpenWrite(args[0] + " native table.txt"));
				foreach (ulong nat in fileopen.X64NativeTable.NativeHashes)
				{
					string temps = nat.ToString("X");
					while (temps.Length < 16)
						temps = "0" + temps;
					fw.WriteLine(temps);
				}
				fw.Flush();
				fw.Close();
				Console.WriteLine("All done & saved!");
			}
		}

		public enum IntType
		{
			_int,
			_uint,
			_hex
		}

		public static IntType Find_getINTType()
		{
			string s = Properties.Settings.Default.IntStyle;
			if (s.StartsWith("int")) return _getINTType = IntType._int;
			else if (s.StartsWith("uint")) return _getINTType = IntType._uint;
			else if (s.StartsWith("hex")) return _getINTType = IntType._hex;
			else
			{
				return _getINTType = IntType._int;
			}
		}

		private static IntType _getINTType = IntType._int;

		public static IntType getIntType
		{
			get { return _getINTType; }
		}
	}
}
