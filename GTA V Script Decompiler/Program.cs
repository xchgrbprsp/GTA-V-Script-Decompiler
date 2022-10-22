using Decompiler.Hooks;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Decompiler
{
	internal static class Program
	{
		public static Crossmap Crossmap;
		public static NativeDB NativeDB;
		public static FunctionDB FunctionDB;
		public static TextDB TextDB;
		public static GlobalTypeMgr GlobalTypeMgr;
		public static Hashes Hashes;

		public static FunctionHook[] FunctionHooks = FunctionHook.GetHooks();
		public static NativeHook[] NativeHooks = NativeHook.GetHooks();

		public static bool ShouldShiftVariables = Properties.Settings.Default.ShiftVariables;
		public static bool ShouldReverseHashes = Properties.Settings.Default.ReverseHashes;
		public static int EnumDisplayTypeCache = Properties.Settings.Default.EnumDisplayType;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
			CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

			Crossmap = new Crossmap();

			Hashes = new Hashes();

			NativeDB = new NativeDB();
			NativeDB.LoadData();

			FunctionDB = new FunctionDB();

			TextDB = new TextDB();

			GlobalTypeMgr = new GlobalTypeMgr();

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
					fileopen.Decompile().Wait();
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error decompiling script " + ex.Message);
					return;
				}

				Console.WriteLine("Decompiled in " + (DateTime.Now - Start).ToString());
				fileopen.Save(File.OpenWrite(args[0] + ".c"), true);

				Console.WriteLine("Extracting native table...");
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
