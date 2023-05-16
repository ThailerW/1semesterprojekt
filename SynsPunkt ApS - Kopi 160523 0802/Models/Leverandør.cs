using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Leverandør
    {
        public int CVRnummer { get; set; }
        public string LeverandørNavn { get; set; }
        public string Adresse { get; set; }
        public int PostNummer { get; set; }
        public int TelefonNummer { get; set; }
        public string Email { get; set; }
        public string BankNavn { get; set; }
        public int Registeringsnummer { get; set; }
        public int KontoNummer { get; set; }

        public Leverandør(int cVRnummer, string leverandørNavn, string adresse, int postNummer, int telefonNummer, 
            string email, string bankNavn, int registeringsnummer, int kontoNummer)
        {
            CVRnummer = cVRnummer;
            LeverandørNavn = leverandørNavn;
            Adresse = adresse;
            PostNummer = postNummer;
            TelefonNummer = telefonNummer;
            Email = email;
            BankNavn = bankNavn;
            Registeringsnummer = registeringsnummer;
            KontoNummer = kontoNummer;
        }
    }
}
