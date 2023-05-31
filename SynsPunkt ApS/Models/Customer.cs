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
        /// <summary>
        /// Får eller sætter kundens fornavn.
        /// </summary>
        public string firstName { get; set; }

        /// <summary>
        /// Får eller sætter kundens efternavn.
        /// </summary>
        public string lastName { get; set; }

        /// <summary>
        /// Får eller sætter kundens telefonnummer.
        /// </summary>
        public int phoneNumber { get; set; }

        /// <summary>
        /// Får eller sætter kundens mailadresse.
        /// </summary>
        public string mail { get; set; }

        /// <summary>
        /// Får eller sætter kundens adresse.
        /// </summary>
        public string adress { get; set; }

        /// <summary>
        /// Får eller sætter kundens id.
        /// </summary>
        public string customerID { get; set; }

        /// <summary>
        /// Får eller sætter kundens postnummer.
        /// </summary>
        public int zipCode { get; set; }

        /// <summary>
        /// Minh: Får eller sætter kundens lokations-id.
        /// </summary>
        public string locationID { get; set; }

        /// <summary>
        /// Minh: Opretter en ny instans af Customer-klassen.
        /// </summary>
        /// <param name="firstName">Kundens fornavn.</param>
        /// <param name="lastName">Kundens efternavn.</param>
        /// <param name="phoneNumber">Kundens telefonnummer.</param>
        /// <param name="mail">Kundens mailadresse.</param>
        /// <param name="adress">Kundens adresse.</param>
        /// <param name="customerID">Kundens id.</param>
        /// <param name="zipCode">Kundens postnummer.</param>
        /// <param name="locationID">Kundens lokations-id.</param>
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
