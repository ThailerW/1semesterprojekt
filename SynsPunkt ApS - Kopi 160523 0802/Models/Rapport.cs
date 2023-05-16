using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Rapport
    {
        public int RapportNummer { get; set; }
        public DateTime StartInterval { get; set; }
        public DateTime SlutInterval { get; set; }
        public List<Models.Kunde> KundeRapport { get; set; }
        public List<Models.Ordre> OrdreRapport { get; set; }
        public List<Models.Vare> VareRapport { get; set; }

        public Rapport(int rapportNummer, DateTime startInterval, DateTime slutInterval)
        {
            RapportNummer = rapportNummer;
            StartInterval = startInterval;
            SlutInterval = slutInterval;
        }
    }
}
