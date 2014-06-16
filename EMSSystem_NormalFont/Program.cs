using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EMSSystem.Functions;

namespace EMSSystem
{
    static class MainProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmEMS());

            FacadeLayer facade = new FacadeLayer("");
            string msg = facade.FacadeFunctions("check", "checksystemislegal", Application.StartupPath, null).ToString();

            if (msg == "")
                Application.Run(new frmLogin());
            else
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
