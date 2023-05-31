using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Customer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int phoneNumber { get; set; }
        public string mail { get; set; }
        public string adress { get; set; }
        public string customerID { get; set; }
        public int zipCode { get; set; }
        public string locationID { get; set; }

        public Customer(string firstName, string lastName, int phoneNumber, string mail, string adress, string customerID, int zipCode, string locationID)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.mail = mail;
            this.adress = adress;
            this.customerID = customerID;
            this.zipCode = zipCode;
            this.locationID = locationID;
        }
    }
}
