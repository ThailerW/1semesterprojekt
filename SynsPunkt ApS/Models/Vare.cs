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
        public decimal Pris { get; set; }

        public Vare(int vareNummer, string vareBeskrivelse, int lagermængde, string varenavn, decimal styrke, string levcvr, decimal pris)
        {
            this.VareNummer = vareNummer;
            this.VareBeskrivelse = vareBeskrivelse;
            this.LagerMængde = lagermængde;
            this.VareNavn = varenavn;
            this.Styrke = styrke;
            this.LevCVR = levcvr;
            this.Pris = pris;
        }
    }
}
