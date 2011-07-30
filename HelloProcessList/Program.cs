using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HelloProcessList
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainFormCmd cmd = new MainFormCmd();
            Application.Run(new MainForm(cmd));
        }
    }
}
