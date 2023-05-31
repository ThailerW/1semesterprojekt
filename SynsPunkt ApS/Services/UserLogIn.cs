using SynsPunkt_ApS.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Services
{
    public class UserLogIn
    {
        /// <summary>
        /// Theis: Checker om brugerens indtastede oplysninger er tilsvarende med data i databasen.
        /// Martin: Tilføjet Ansat instans som tilføjes som parameter til MainMenu Constructor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>True hvis login er successfuld, ellers false</returns>
        public static bool GetUserLogin(string username, string password)
        {
            bool validUser = Database.CheckerUserInfo.CheckUserInfoDB(username, password);

            if (validUser)
            {
                Services.Ansat_Services ansatService = new Services.Ansat_Services();
                Models.Employee loggedInEmployee = ansatService.GetAnsatByID(username);
                MainMenu mainMenu = new MainMenu(loggedInEmployee);
                mainMenu.Show();
                MessageBox.Show("Login success!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
