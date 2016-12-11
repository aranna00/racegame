using System;
using System.Windows.Forms;

namespace RaceGame2
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
            Form1 startForm = new Form1();
            Application.Run(startForm);
        }
    }
}
