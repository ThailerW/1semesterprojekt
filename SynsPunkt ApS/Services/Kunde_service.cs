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
    public class Kunde_Services
    {
        private CRUD_Kunde crudKunde;

        public Kunde_Services()
        {
            crudKunde = new Database.CRUD_Kunde();
        }

        public void CreateKunde(string lokationId, string Mail, string forNavn, string efterNavn, int telefonNummer, string adresse, int postNr)
        {
            // Opret en ny kunde
            crudKunde.CreateKunde(lokationId, Mail, forNavn, efterNavn, telefonNummer, adresse, postNr);
        }

        public void UpdateKunde(string lokationId, int KundeID, string Mail, string forNavn, string efterNavn, int telefonNummer, string adresse, int postNr)
        {
            crudKunde.UpdateKunde(lokationId, KundeID, Mail, forNavn, efterNavn, telefonNummer, adresse, postNr);

        }



        public void DeleteKunde(int KundeID)
        {
            // Slet en kunde
            crudKunde.DeleteKunde(KundeID);

            // Opdateringslogikken for kunden kan placeres her
        }

        public List<Customer> GetCustomers()
        {
            // Hent alle kunder
            return crudKunde.GetCustomers();
        }

        public Customer GetKunde(int KundeID)
        {
            Database.CRUD_Kunde crudKunde = new Database.CRUD_Kunde();
            return crudKunde.HentKunde(KundeID);
        }

        public bool CheckIfCustomerExists(int kundeID)
        {
            return crudKunde.CheckIfCustomerExists(kundeID);
        }
    }
}

