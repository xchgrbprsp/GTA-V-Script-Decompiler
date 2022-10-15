using System;
using System.IO;
using System.Windows.Forms;

namespace Decompiler
{
	static class Program
	{
		public static x64NativeFile x64nativefile;
		public static object ThreadLock;
		public static NativeDB nativeDB;
		public static FunctionDB functionDB;
		public static TextDB textDB;
		public static GlobalTypeMgr globalTypeMgr;

		public static bool shouldShiftVariables = Properties.Settings.Default.ShiftVariables;
		public static bool shouldReverseHashes = Properties.Settings.Default.ReverseHashes;

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

			functionDB = new FunctionDB();

			textDB = new TextDB();

			globalTypeMgr = new GlobalTypeMgr();

			if (args.Length == 0)
			{
				Application.EnableVisualStyles();
				Application.SetHighDpiMode(HighDpiMode.SystemAware);
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
			else
			{
				var Start = DateTime.Now;
				var ext = Path.GetExtension(args[0]);
				if (ext == ".full")
				{
					ext = Path.GetExtension(Path.GetFileNameWithoutExtension(args[0]));
				}

				ScriptFile fileopen;
				Console.WriteLine("Decompiling " + args[0] + "...");
				try
				{
					fileopen = new ScriptFile(File.OpenRead(args[0]));
					var task = fileopen.Decompile();
					task.Wait();
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error decompiling script " + ex.Message);
					return;
				}

				Console.WriteLine("Decompiled in " + (DateTime.Now - Start).ToString());
				fileopen.Save(File.OpenWrite(args[0] + ".c"), true);
				Console.WriteLine("Extracing native table...");
				StreamWriter fw = new(File.OpenWrite(args[0] + " native table.txt"));
				foreach (var nat in fileopen.X64NativeTable.NativeHashes)
				{
					var temps = nat.ToString("X");
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
			var s = Properties.Settings.Default.IntStyle;
			return s.StartsWith("int")
				? (getIntType = IntType._int)
				: s.StartsWith("uint")
				? (getIntType = IntType._uint)
				: s.StartsWith("hex") ? (getIntType = IntType._hex) : (getIntType = IntType._int);
		}

		public static IntType getIntType { get; private set; } = IntType._int;
	}
}
