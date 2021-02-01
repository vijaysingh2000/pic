using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsuranceCalculator
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

            Login login = new Login();
            if (login.ShowDialog() == DialogResult.OK)
            {
                if (CommonData.LoggedInUser.IsAdmin)
                {
                    Application.Run(new Admin());
                }
                else
                {
                    Application.Run(new Main());
                }
            }
        }
    }
}
