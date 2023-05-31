using SynsPunkt_ApS.Database;
using SynsPunkt_ApS.Models;
using SynsPunkt_ApS.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Services
{


    public class Customer_service
    {
        private CRUD_Customer crudCustomer;

        public Customer_service()
        {
            /// <summary>
            /// Minh: Konstruktør for Customer_service-klassen.
            /// Initialiserer CRUD_Customer-objektet.
            /// </summary>
            crudCustomer = new Database.CRUD_Customer();
        }

        /// <summary>
        /// Minh: Opretter en ny kunde.
        /// </summary>
        /// <param name="locationID">Stedets ID for kunden.</param>
        /// <param name="Mail">Kundens e-mailadresse.</param>
        /// <param name="firstName">Kundens fornavn.</param>
        /// <param name="lastName">Kundens efternavn.</param>
        /// <param name="phoneNumber">Kundens telefonnummer.</param>
        /// <param name="adress">Kundens adresse.</param>
        /// <param name="zipCode">Kundens postnummer.</param>
        public void CreateCustomer(string locationID, string Mail, string firstName, string lastName, int phoneNumber, string adress, int zipCode)
        {
            // Opret en ny kunde
            crudCustomer.CreateCustomer(locationID, Mail, firstName, lastName, phoneNumber, adress, zipCode);
        }

        /// <summary>
        /// Minh: Opdaterer en eksisterende kunde.
        /// </summary>
        /// <param name="locationID">Stedets ID for kunden.</param>
        /// <param name="customerID">Kundens ID.</param>
        /// <param name="Mail">Kundens e-mailadresse.</param>
        /// <param name="firstName">Kundens fornavn.</param>
        /// <param name="lastName">Kundens efternavn.</param>
        /// <param name="phoneNumer">Kundens telefonnummer.</param>
        /// <param name="adress">Kundens adresse.</param>
        /// <param name="zipCode">Kundens postnummer.</param>
        public void UpdateCustomer(string locationID, int customerID, string Mail, string firstName, string lastName, int phoneNumer, string adress, int zipCode)
        {
            crudCustomer.UpdateCustomer(locationID, customerID, Mail, firstName, lastName, phoneNumer, adress, zipCode);
        }

        /// <summary>
        /// Minh: Sletter en kunde.
        /// </summary>
        /// <param name="customerID">Kundens ID.</param>
        public void DeleteCustomer(int customerID)
        {
            // Slet en kunde
            crudCustomer.DeleteCustomer(customerID);

            // Opdateringslogikken for kunden kan placeres her
        }

        /// <summary>
        /// Minh: Henter alle kunder.
        /// </summary>
        /// <returns>En liste af alle kunder.</returns>
        public List<Customer> GetCustomers()
        {
            // Hent alle kunder
            return crudCustomer.GetCustomers();
        }

        /// <summary>
        /// Minh: Henter en kunde baseret på ID.
        /// </summary>
        /// <param name="customerID">Kundens ID.</param>
        /// <returns>Kunden, der svarer til det angivne ID.</returns>
        public Customer GetCustomer(int customerID)
        {
            Database.CRUD_Customer crudKunde = new Database.CRUD_Customer();
            return crudKunde.GetCustomer(customerID);
        }

        /// <summary>
        /// Martin: Takes a single input, uses database method to check if a customer exists in the database, returns true or false based on that
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool CheckIfCustomerExists(int customerID)
        {
            return crudCustomer.CheckIfCustomerExists(customerID);
        }
    }
}

