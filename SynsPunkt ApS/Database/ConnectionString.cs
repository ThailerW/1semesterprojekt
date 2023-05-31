using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Database
{
    public class ConnectionString
    {
        /// <summary>
        /// Theis: Gets the connection string to the DB and returns it.
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            string connectionString = "Data Source=mssql15.unoeuro.com;Initial Catalog=oversaftigt_dk_db_test;User ID=oversaftigt_dk;Password=prfHFR9546nEtyb2xDmc";
            return connectionString;
        }
    }
}
