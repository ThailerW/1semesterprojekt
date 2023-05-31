using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Rolle
    {
        public string roleName { get; set; }
        public Models.Permissions permissions { get; set; }
    }
}
