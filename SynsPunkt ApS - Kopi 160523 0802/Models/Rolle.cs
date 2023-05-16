using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Rolle
    {
        public string Rollenavn { get; set; }
        public Models.Permissions Rettigheder { get; set; }
    }
}
