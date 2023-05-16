using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Vare
    {
        public int VareNummer { get; set; }
        public string VareBeskrivelse { get; set; }
        public string VareNavn { get; set; }
        public string VareGruppe { get; set; }
        public decimal Styrke { get; set; }
        public string Farve { get; set; }

        public Vare(int vareNummer, string vareBeskrivelse, string vareNavn, string varegruppe)
        {
            VareNummer = vareNummer;
            VareBeskrivelse = vareBeskrivelse;
            VareNavn = vareNavn;
            VareGruppe = varegruppe;
        }
    }
}
