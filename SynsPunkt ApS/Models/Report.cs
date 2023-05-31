using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Report
    {
        public int reportNumber { get; set; }
        public DateTime startInterval { get; set; }
        public DateTime endInterval { get; set; }
        public List<Models.Customer> customerReport { get; set; }
        public List<Models.Order> orderReport { get; set; }
        public List<Models.Product> productReport { get; set; }

        public Report(int reportNumber, DateTime startInterval, DateTime endInterval)
        {
            this.reportNumber = reportNumber;
            this.startInterval = startInterval;
            this.endInterval = endInterval;
        }
    }
}
