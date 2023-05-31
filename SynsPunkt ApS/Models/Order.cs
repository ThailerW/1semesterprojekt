using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Order
    {
        public int orderID { get; set; }
        public int customerID { get; set; }
        public DateTime orderDate { get; set; }
        public double totalPrice { get; set; }
        //public List<Models.VareLinje> SamletVare { get; set; }

        public Order(int orderID,int customerID ,DateTime orderDate, double totalPrice)
        {
            this.customerID = customerID;
            this.orderID = orderID;
            this.orderDate = orderDate;
            this.totalPrice = totalPrice;
        }
    }
}