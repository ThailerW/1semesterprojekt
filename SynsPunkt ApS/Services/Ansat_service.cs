using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class Ansat_Services
    {
        private Database.CRUD_Employee crudAnsat = new Database.CRUD_Employee();

        public Ansat_Services()
        {

        }

        public void CreateAnsat(string fornavn, string efternavn, int telefonNummer, string privatMail, string adresse,
        string adgangskode, string afdeling, string rolle, string arbejdsMail, int postNr)
        {
            crudAnsat.CreateEmployee(fornavn, efternavn, telefonNummer, privatMail, adresse, adgangskode, afdeling, rolle, arbejdsMail, postNr);

        }

        public void UpdateAnsat(int medarbejderNummer, string fornavn, string efternavn, int telefonNummer, string privatMail, string adresse,
        string adgangskode, string afdeling, string rolle, string arbejdsMail, int postNr)
        {
            crudAnsat.UpdateEmployee(medarbejderNummer, fornavn, efternavn, telefonNummer, privatMail, adresse,
                                  adgangskode, afdeling, rolle, arbejdsMail, postNr);
        }

        public void DeleteAnsat(int medarbejderNummer)
        {
            crudAnsat.DeleteEmployee(medarbejderNummer);
        }

        public Models.Employee GetAnsatByID(string id)
        {
            Models.Employee ansat = crudAnsat.GetEmployeeByID(id);
            return ansat;
        }

        public List<Models.Employee> SearchAnsatByName(string name)
        {
            List<Models.Employee> searchResults = crudAnsat.SearchEmployeeByName(name);

            return searchResults;
        }

        public List<Models.Employee> GetAllAnsat()
        {
            List<Models.Employee> allAnsat = crudAnsat.GetAllEmployees();

            return allAnsat;
        }

        public string GetDepartmentName(int departmentID)
        {
            string departmentName = crudAnsat.GetDepartmentName(departmentID);

            return departmentName;
        }
        public string GetRoleName(int roleID)
        {
            string roleName = crudAnsat.GetRoleName(roleID);

            return roleName;
        }
    }
}
