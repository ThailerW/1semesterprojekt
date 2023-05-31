using SynsPunkt_ApS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    internal class Ordre_service
    {
        private Database.CRUD_Order crudOrder = new Database.CRUD_Order();

        /// <summary>
        /// Martin: Takes inputs, uses database method to create an order, and then returns the OrderID of the order that was just created
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="orderDate"></param>
        /// <param name="totalPrice"></param>
        /// <returns></returns>
        public int CreateOrder(int customerID, DateTime orderDate, double totalPrice)
        {
            int orderID = crudOrder.CreateOrder(customerID, orderDate, totalPrice);
            return orderID;
        }

        /// <summary>
        /// Martin: Uses database method to get a list of orders and then returns said list
        /// </summary>
        /// <returns></returns>
        public List<Models.Order> GetAllOrders()
        {
            return crudOrder.GetAllOrders();
        }

        
    }
}
