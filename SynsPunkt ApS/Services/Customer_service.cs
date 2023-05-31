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
            crudCustomer = new Database.CRUD_Customer();
        }

        public void CreateCustomer(string locationID, string Mail, string firstName, string lastName, int phoneNumber, string adress, int zipCode)
        {
            // Opret en ny kunde
            crudCustomer.CreateCustomer(locationID, Mail, firstName, lastName, phoneNumber, adress, zipCode);
        }

        public void UpdateCustomer(string locationID, int customerID, string Mail, string firstName, string lastName, int phoneNumer, string adress, int zipCode)
        {
            crudCustomer.UpdateCustomer(locationID, customerID, Mail, firstName, lastName, phoneNumer, adress, zipCode);

        }

        public void DeleteCustomer(int customerID)
        {
            // Slet en kunde
            crudCustomer.DeleteCustomer(customerID);

            // Opdateringslogikken for kunden kan placeres her
        }

        public List<Customer> GetCustomers()
        {
            // Hent alle kunder
            return crudCustomer.GetCustomers();
        }

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

