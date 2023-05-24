using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class VareService
    {
        public void CreateVare(string vareBeskrivelse, int lagerMængde, string vareNavn, decimal styrke, string levCVR, decimal pris)
        {
            Database.CRUD_Vare crudVare = new Database.CRUD_Vare();
            crudVare.CreateVare(vareBeskrivelse, lagerMængde, vareNavn, styrke, levCVR, pris);
        }

        public void ReadVare(string id, out string id2, out string vareBeskrivelse, out string lagerMængde, out string vareNavn,
            out string styrke, out string levCVR, out string pris)
        {
            Database.CRUD_Vare crudVare = new Database.CRUD_Vare();
            crudVare.ReadVare(id, out id2, out vareBeskrivelse, out lagerMængde, out vareNavn, out styrke, out levCVR, out pris);
        }

        public void UpdateVare(string vareID, string vareBeskrivelse, int lagerMængde, string vareNavn, decimal styrke, string levCVR, decimal pris)
        {
            Database.CRUD_Vare crudVare = new Database.CRUD_Vare();
            crudVare.UpdateVare(vareID, vareBeskrivelse, lagerMængde, vareNavn, styrke, levCVR, pris);
        }

        public void DeleteVare(string vareID)
        {
            Database.CRUD_Vare crudVare = new Database.CRUD_Vare();
            crudVare.DeleteVare(vareID);
        }

        public List<Models.Vare> SearchVareByName(string name)
        {
            Database.CRUD_Vare crudVare = new Database.CRUD_Vare();
            return crudVare.SearchVareByName(name);
        }

        public List<Models.Vare> GetAllVare()
        {
            Database.CRUD_Vare crudVare = new Database.CRUD_Vare();
            var AlleVare = crudVare.GetAllVare();
            return AlleVare;
        }
    }
}
