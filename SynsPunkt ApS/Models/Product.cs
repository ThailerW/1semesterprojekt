using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Product
    {
        public int productNumber { get; set; }
        public string productDescription { get; set; }
        public string productName { get; set; }
        public decimal lensStrength { get; set; }
        public string color { get; set; }
        public int stockQuantity { get; set; }
        public string levCVR { get; set; }
        public decimal price { get; set; }

        public Product(int productNumber, string productDescription, int stockQuantity, string productName, decimal lensStrength, string levcvr, decimal price)
        {
            this.productNumber = productNumber;
            this.productDescription = productDescription;
            this.stockQuantity = stockQuantity;
            this.productName = productName;
            this.lensStrength = lensStrength;
            this.levCVR = levcvr;
            this.price = price;
        }
    }
}
