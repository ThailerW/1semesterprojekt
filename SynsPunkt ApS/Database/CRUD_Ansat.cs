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

        public string GetNameAndRights(int AnsatID, out int rolleID)
        {
            SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());
            string sSQL = $"SELECT fornavn, efternavn, rolleID FROM SP_Ansat WHERE AnsatID = {AnsatID}";
            SqlCommand command = new SqlCommand(sSQL, conn);
            SqlDataReader reader = command.ExecuteReader();

            string fullName = string.Empty;
            rolleID = 0;
            while (reader.Read())
            {
                fullName = reader.GetString(0) + " " + reader.GetString(1);
                rolleID = reader.GetInt32(2);
            }

            return fullName;
        }
    }
}
