using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ChatBot.Models
{
    public class Briller
    {
        public string Navn { get; set; }
        public double BrilleBredde { get; set; }
        public double StængerLængde { get; set; }
        public string StelFarve { get; set; }
        public double GlasDiameter { get; set; }
        public bool ErMærkevare { get; set; }


        public Briller(string navn, double brillebredde, double stængerlængde, string stelfarve, double glasdiameter, bool ermærkevare)
        {
            this.Navn = navn;
            this.BrilleBredde = brillebredde;
            this.StængerLængde = stængerlængde;
            this.StelFarve = stelfarve;
            this.GlasDiameter = glasdiameter;
            this.ErMærkevare = ermærkevare;
        }
    }
}
