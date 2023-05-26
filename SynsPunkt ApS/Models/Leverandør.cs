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
        public string FaktureringsOplysninger { get; set; }

        public Leverandør(int cVRnummer, string leverandørNavn, string adresse, int postNr, string email, string faktureringsoplysninger, int telefonNummer)        {
            this.CVRnummer = cVRnummer;
            this.LeverandørNavn = leverandørNavn;
            this.Adresse = adresse;
            this.PostNummer = postNr;
            this.Email = email;
            this.FaktureringsOplysninger = faktureringsoplysninger;
            this.TelefonNummer = telefonNummer;
        }

    }
}
