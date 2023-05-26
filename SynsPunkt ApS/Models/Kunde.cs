using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Kunde
    {
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public int TelefonNummer { get; set; }
        public string Mail { get; set; }
        public string Adresse { get; set; }
        public string KundeId { get; set; }
        public int PostNr { get; set; }
        public string LokationId { get; set; }

        public Kunde(string fornavn, string efternavn, int telefonNummer, string mail, string adresse, string kundeId, int postNr, string lokationId)
        {
            Fornavn = fornavn;
            Efternavn = efternavn;
            TelefonNummer = telefonNummer;
            Mail = mail;
            Adresse = adresse;
            KundeId = kundeId;
            PostNr = postNr;
            LokationId = lokationId;
        }
    }
}
