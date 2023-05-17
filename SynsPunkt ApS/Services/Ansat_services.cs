using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class Ansat_Services
    {
        static int userID;
        public static void SetUserID(string UserID)
        {
            userID = int.Parse(UserID);
        }

        public static int GetUserID()
        {
            return userID;
        }
    }
}
