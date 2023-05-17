﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SynsPunkt_ApS.Database
{
    public class CheckerUserInfo
    {
        /// <summary>
        /// THEIS: Checker om input data er tilsvarende med data gemt i databasen.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        public static bool CheckUserInfoDB(string userID, string password)
        {
            string connectionString = Database.ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                //Hvis username og password stemmer overens med hinanden vil denne query returnere 1, eller 0 hvis der
                //ikke opstår et match.
                //string query = "SELECT COUNT(*) FROM SP_Ansat WHERE AnsatID = @userID AND password = @password";

                //string query = "SELECT COUNT(*) FROM SP_Ansat WHERE AnsatID = '" + userID + "' AND password = '" + password + "'";

                string query = $"SELECT COUNT(*) FROM SP_Ansat WHERE AnsatID = '{userID}' AND password = '{password}'";


                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue("@userID", userID);
                //command.Parameters.AddWithValue("@password", password);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                if (count == 1)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Ugyldigt brugernavn eller password!", "FEJL!", MessageBoxButtons.OK);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex, "FEJL!", MessageBoxButtons.OK);
                return false;
            }
            finally 
            { 
                connection.Close();
            }
        }
    }
}
