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
        public decimal Styrke { get; set; }
        public string Farve { get; set; }
        public int LagerMængde { get; set; }
        public string LevCVR { get; set; }

        public Vare(int vareNummer, string vareBeskrivelse, int lagermængde, string varenavn, decimal styrke, string levcvr)
        {
            VareNummer = vareNummer;
            VareBeskrivelse = vareBeskrivelse;
            LagerMængde = lagermængde;
            VareNavn = varenavn;
            Styrke = styrke;
            LevCVR = levcvr;
        }
    }
}
