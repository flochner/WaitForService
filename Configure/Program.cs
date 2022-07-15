using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace Configure
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            /// This section exists to delete the placed shortcut during uninstall.  
            try
            {
                if (Convert.ToBoolean(args[0]))
                {
                    RegistryKey regKeyConfig = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ConRes\WaitForService", true);
                    string path = (string)regKeyConfig.GetValue("ShortcutPath");
                    if (!string.IsNullOrEmpty(path))
                    {
                        FileInfo shortcut = new FileInfo(path);
                        shortcut.Delete();
                    }
                }
                Environment.Exit(0);
            }
            catch (IndexOutOfRangeException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(-1);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Configure());
        }
    }
}
