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
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void GetUserLogin(string username, string password)
        {
            bool validUser = Database.CheckerUserInfo.CheckUserInfoDB(username, password);

            if (validUser)
            {
                MessageBox.Show("Login succes!");
            }
        }
    }
}
