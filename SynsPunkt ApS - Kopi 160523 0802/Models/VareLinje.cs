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

        public VareLinje(int vareLinjeID, Models.Vare vare, int mængde)
        {
            this.VareLinjeID = vareLinjeID;
            Vare = vare;
            Mængde = mængde;
        }   
    }
}
