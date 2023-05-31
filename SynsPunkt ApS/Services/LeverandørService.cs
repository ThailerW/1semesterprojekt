using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class LeverandørService
    {

        public void CreateLeverandør(string navn, string adresse, int postNr, string email, string faktureringsinfo, int tlfNummer)
        {
            Database.CRUD_Leverandør crudLev = new Database.CRUD_Leverandør();
            crudLev.CreateLeverandør(navn, adresse, postNr, email, faktureringsinfo, tlfNummer);
        }

        public void ReadLeverandør(string levId, out string levId2, out string navn, out string adresse, out string postNr, out string email, 
            out string faktureringsinfo, out string tlfNummer)
        {
            Database.CRUD_Leverandør crudLev = new Database.CRUD_Leverandør();
            crudLev.ReadLeverandør(levId, out levId2, out navn, out adresse, out postNr, out email, out faktureringsinfo, out tlfNummer);
        }

        public void UpdateLeverandør(string levId, string navn, string adresse, int postNr, string email, string faktureringsinfo, int tlfNummer)
        {
            Database.CRUD_Leverandør crudLev = new Database.CRUD_Leverandør();
            crudLev.UpdateLeverandør(levId, navn, adresse, postNr, email, faktureringsinfo, tlfNummer);
        }

        public void DeleteLeverandør(string LeverandørID)
        {
            Database.CRUD_Leverandør crudLev = new Database.CRUD_Leverandør();
            crudLev.DeleteLeverandør(LeverandørID);
        }

        public List<Models.Supplier> SearchLeverandørByName(string name)
        {
            Database.CRUD_Leverandør crudLev = new Database.CRUD_Leverandør();
            return crudLev.SearchLeverandørByName(name);
        }

        public List<Models.Supplier> GetAllLeverandør()
        {
            Database.CRUD_Leverandør crudLev = new Database.CRUD_Leverandør();
            var AlleLeverandør = crudLev.GetAllLeverandør();
            return AlleLeverandør;
        }

    }
}
