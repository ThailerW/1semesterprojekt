using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class VareService
    {
        public void CreateVare(string vareBeskrivelse, int lagerMængde, string vareNavn, decimal styrke, string levCVR)
        {
            Database.CRUD_Vare crudVare = new Database.CRUD_Vare();
            crudVare.CreateVare(vareBeskrivelse, lagerMængde, vareNavn, styrke, levCVR);
        }

        /*public void ReadVare(string vareID, out string displayID, out string vareBeskrivelse, out int lagerMængde, 
            out string vareNavn, out decimal styrke, out string levCVR)
        {
            Database.CRUD_Vare crudVare = new Database.CRUD_Vare();
        }
        */

        public void UpdateVare(string vareID, string vareBeskrivelse, int lagerMængde, string vareNavn, decimal styrke, string levCVR)
        {
            Database.CRUD_Vare crudVare = new Database.CRUD_Vare();
        }

        public void DeleteVare(string vareID)
        {
            Database.CRUD_Vare crudVare = new Database.CRUD_Vare();
        }

        public List<Models.Vare> GetAllVare()
        {
            Database.CRUD_Vare crudVare = new Database.CRUD_Vare();
            List<Models.Vare> AlleVare = new List<Models.Vare>();
            return AlleVare;
        }
    }
}
