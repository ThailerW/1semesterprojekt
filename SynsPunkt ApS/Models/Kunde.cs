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
        public string PrivatEmail { get; set; }
        public string Adresse { get; set; }
        public string KundeNummer { get; set; }
        public string KundeInfo { get; set; }
        public int PostNr { get; set; }
        public List<Models.Ordre> OrdeHistorik { get; set; }
        public List <Models.Booking> BookingHistorik { get; set; }

        public Kunde(string fornavn, string efternavn, int telefonNummer, string privatEmail, string adresse, 
            string kundeNummer, string kundeInfo, int postNr)
        {
            Fornavn = fornavn;
            Efternavn = efternavn;
            TelefonNummer = telefonNummer;
            PrivatEmail = privatEmail;
            Adresse = adresse;
            KundeNummer = kundeNummer;
            KundeInfo = kundeInfo;
            PostNr = postNr;
        }
    }
}
