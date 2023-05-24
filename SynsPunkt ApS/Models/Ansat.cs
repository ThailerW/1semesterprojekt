using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Ansat
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TelephoneNumber { get; set; }
        public string PrivateMail { get; set; }
        public string Adress { get; set; }
        public int EmployeeID { get; set; }
        public string Password { get; set; }
        public string DepartmentID { get; set; }
        public int RoleID { get; set; }
        public string WorkMail { get; set; }
        public int ZipCode { get; set; }

        public Ansat(string firstName, string lastName, int telephoneNumber, string privateMail, string adress,
            int employeeID, string password, string departmentID, int roleID, string workMail, int zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            TelephoneNumber = telephoneNumber;
            PrivateMail = privateMail;
            Adress = adress;
            EmployeeID = employeeID;
            Password = password;
            DepartmentID = departmentID;
            RoleID = roleID;
            WorkMail = workMail;
            ZipCode = zipCode;
        }
    }
}
