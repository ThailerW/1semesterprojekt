using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Ansat
    {
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public int TelefonNummer { get; set; }
        public string PrivatMail { get; set; }
        public string Adresse { get; set; }
        public int MedarbejderNummer { get; set; }
        public string Adgangskode { get; set; }
        public string Afdeling { get; set; }
        public int Rolle { get; set; }
        public string ArbejdsMail { get; set; }
        public int PostNr { get; set; }

        public Ansat(string fornavn, string efternavn, int telefonNummer, string privatEmail, string adresse, 
            int medarbejderNummer, string adgangskode, string afdeling, int rolle, string arbejdsMail, int postNr)
        {
            Fornavn = fornavn;
            Efternavn = efternavn;
            TelefonNummer = telefonNummer;
            PrivatMail = privatEmail;
            Adresse = adresse;
            MedarbejderNummer = medarbejderNummer;
            Adgangskode = adgangskode;
            Afdeling = afdeling;
            Rolle = rolle;
            ArbejdsMail = arbejdsMail;
        }
    }
}
