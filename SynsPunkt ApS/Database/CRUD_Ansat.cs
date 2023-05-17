using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Database
{
    public class CRUD_Ansat
    {
        // Logik
        public void CreateAnsat ()
        {
            // Logik
        }
        
        public void ReadAnsat ()
        {
            // Logik
        }
        // Logik
        public void UpdateAnsat () 
        {
            // Logik
        }
      
        public void DeleteAnsat ()
        {
            // Logik

        }

        //public string GetNameAndRights(int AnsatID, out int rolleID)
        //{
        //    SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());
        //    string sSQL = $"SELECT fornavn, efternavn, rolleID FROM SP_Ansat WHERE AnsatID = {AnsatID}";
        //    SqlCommand command = new SqlCommand(sSQL, conn);
        //    SqlDataReader reader = command.ExecuteReader();

        //    string fullName = string.Empty;
        //    rolleID = 0;
        //    while (reader.Read())
        //    {
        //        fullName = reader.GetString(0) + " " + reader.GetString(1);
        //        rolleID = reader.GetInt32(2);
        //    }

        //    return fullName;
        //}

        /// <summary>
        /// Martin: Retunerer en instans af Ansat med den logged ind persons data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.Ansat GetEmployeeData(string id)
        {
            Models.Ansat Employee = null;

            SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());
            string query = "SELECT TOP 1 * FROM SP_Ansat WHERE AnsatID = @id";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int medarbejderNummer = reader.GetInt32(0); 
                string afdeling = reader.GetInt32(1).ToString();
                string arbejdsMail = reader.GetString(2);
                string forNavn = reader.GetString(3);
                string efterNavn = reader.GetString(4);
                int tlf = reader.GetInt32(5);
                string adresse = reader.GetString(6);
                int postNummer =  reader.GetInt32(7);
                int rolleID = reader.GetInt32(8);
                string password = reader.GetString(9);
                string privatMail = reader.GetString(10);
                
                Employee = new Models.Ansat(forNavn, efterNavn, tlf, privatMail, adresse, medarbejderNummer, password, afdeling, rolleID, arbejdsMail);
            }
            conn.Close();
            return Employee;
        }
    }
}
