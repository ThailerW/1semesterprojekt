using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Optiker
    {
        public int LokationsID { get; set; }
        public string Adresse { get; set; }
        public string PostNr { get; set; }
        public int TelefonNummer { get; set; }
        public string Email { get; set; }

        public Optiker(int lokationsID, string adresse, string postNr, int telefonNummer, string email)
        {
            LokationsID = lokationsID;
            Adresse = adresse;
            PostNr = postNr;
            TelefonNummer = telefonNummer;
            Email = email;
        }
    }
}
