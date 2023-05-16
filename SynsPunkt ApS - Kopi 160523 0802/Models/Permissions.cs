using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Permissions
    {
        public bool IsEmployee { get; set; }
        public bool IsManager { get; set; }
        public bool IsOwner { get; set; }

        public Permissions(bool isEmployee ,bool isManager, bool isOwner)
        {
            IsEmployee = isEmployee;
            IsManager = isManager;
            IsOwner = isOwner;
        }  
    }
}
