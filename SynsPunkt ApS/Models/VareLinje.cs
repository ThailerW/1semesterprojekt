using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class VareLinje
    {
        public int VareLinjeID { get; set; }
        public Models.Vare Vare { get; set; }
        public int Mængde { get; set; }
        public decimal totalPris;

        public VareLinje(int vareLinjeID, Models.Vare vare, int mængde, decimal totalPris)
        {
            this.VareLinjeID = vareLinjeID;
            this.Vare = vare;
            this.Mængde = mængde;
            this.totalPris = totalPris;
        }

        public VareLinje(Models.Vare vare, int mængde, decimal totalPris)
        {

            this.Vare = vare;
            this.Mængde = mængde;
            this.totalPris = totalPris;
        }
    }
}
