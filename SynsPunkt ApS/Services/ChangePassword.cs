using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Services
{
    public class ChangePassword
    {
        /// <summary>
        /// Theis: Checks if the userid matches the old password, as well as checks if the two new passwords matches, and if both matches
        /// are true, the new password will overwrite the old in the database.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword1"></param>
        /// <param name="newPassword2"></param>
        /// <returns></returns>
        public bool ChangeUserPassword(string userID, string oldPassword, string newPassword1, string newPassword2)
        {
            Database.ChangePassword newPass = new Database.ChangePassword();
            bool confirmed = newPass.ConfirmUser(userID, oldPassword);

            if (confirmed)
            {
                if (newPassword1 == newPassword2)
                {
                    newPass.ChangeNewPassword(userID, newPassword1);
                    return true;
                }
                else
                {
                    MessageBox.Show("Ny adgangskode stemmer ikke overens!", "HOVSA!", MessageBoxButtons.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Forkert nuværende adgangskode!", "HOVSA!", MessageBoxButtons.OK);
                return false;
            }
        }
    }
}
