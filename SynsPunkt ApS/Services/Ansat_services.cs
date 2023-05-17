using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class Ansat_Services
    {
        static string userID;
        public static void SetUserID(string UserID)
        {
            userID = UserID;
        }

        public static string GetUserID()
        {
            return userID;
        }
    }
}
