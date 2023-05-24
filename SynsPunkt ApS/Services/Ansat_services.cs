using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class Ansat_Services
    {
        private Database.CRUD_Ansat crudAnsat;

        public Ansat_Services()
        {
            crudAnsat = new Database.CRUD_Ansat();
        }

        public void CreateAnsat(string fornavn, string efternavn, int telefonNummer, string privatMail, string adresse,
        string adgangskode, string afdeling, int rolle, string arbejdsMail, int postNr)
        {
            crudAnsat.CreateAnsat(fornavn, efternavn, telefonNummer, privatMail, adresse, adgangskode, afdeling, rolle, arbejdsMail, postNr);

        }

        public void UpdateAnsat(int medarbejderNummer, string fornavn, string efternavn, int telefonNummer, string privatMail, string adresse,
        string adgangskode, string afdeling, int rolle, string arbejdsMail, int postNr)
        {
            crudAnsat.UpdateAnsat(medarbejderNummer, fornavn, efternavn, telefonNummer, privatMail, adresse,
                                  adgangskode, afdeling, rolle, arbejdsMail, postNr);
        }

        public void DeleteAnsat(int medarbejderNummer)
        {
            crudAnsat.DeleteAnsat(medarbejderNummer);
        }

        public Models.Ansat GetAnsatByID(string id)
        {
            Models.Ansat ansat = crudAnsat.GetAnsatByID(id);
            return ansat;
        }

        public List<Models.Ansat> SearchAnsatByName(string name)
        {
            List<Models.Ansat> searchResults = crudAnsat.SearchAnsatByName(name);

            return searchResults;
        }

    }
}
