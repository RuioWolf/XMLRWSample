using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
	public class Logger
	{
		static StreamWriter sw;
		static string logFile;
		static string text;

		public static void log(string arg)
		{
			logFile = Environment.CurrentDirectory + "\\log.txt";

//			if (!File.Exists(logFile))
//			{
//				File.Create(logFile);
//			}
//			File.Open(logFile, FileMode.Append);

			text = "[ " + DateTime.Now + " ]: " + arg;
#if (DEBUG)
			MessageBox.Show(text);
#endif
			sw = File.AppendText(logFile);
			sw.WriteLine(text);
			sw.Flush();
			sw.Close();
		}
	}
}