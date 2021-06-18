using System;
using System.Windows.Forms;

namespace WaitForService
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Form1 _obj = new Form1();
            Application.EnableVisualStyles();
            Application.Run(_obj);
        }
    }
}
