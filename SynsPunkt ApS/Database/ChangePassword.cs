using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Database
{
    public class ChangePassword
    {
        public bool ConfirmUser(string userID, string password)
        {
            bool confirmed = CheckerUserInfo.CheckUserInfoDB(userID, password);
            return confirmed;
        }
        public void ChangeNewPassword(string userID, string newPassword)
        {
            string connectionString = ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "UPDATE SP_Ansat SET password = '" + newPassword + "' WHERE AnsatID = '" + userID + "'";
            SqlCommand cmd = connection.CreateCommand();

            try
            {
                cmd.CommandText = query;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "OOPS!", MessageBoxButtons.OK);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
