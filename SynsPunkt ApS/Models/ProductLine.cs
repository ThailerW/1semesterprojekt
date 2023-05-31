using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class ProductLine
    {
        public int productLineID { get; set; }
        public Models.Product product { get; set; }
        public int quantity { get; set; }
        public decimal totalPrice;

        public ProductLine(int productLineID, Models.Product product, int quantity, decimal totalPris)
        {
            this.productLineID = productLineID;
            this.product = product;
            this.quantity = quantity;
            this.totalPrice = totalPris;
        }

        public ProductLine(Models.Product vare, int mængde, decimal totalPris)
        {

            this.product = vare;
            this.quantity = mængde;
            this.totalPrice = totalPris;
        }
    }
}
