using SynsPunkt_ApS.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Ny loginform som vælges til at være den form der skal oprettes når programmet kører
            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);
        }
    }
}
