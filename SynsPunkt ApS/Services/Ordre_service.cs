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
        private Database.CRUD_Ordre crudOrdre = new Database.CRUD_Ordre();

        public int CreateOrder(int kundeID, DateTime orderDate, double totalPrice)
        {
            int orderID = crudOrdre.CreateOrdre(kundeID, orderDate, totalPrice);
            return orderID;
        }

        public List<Models.Ordre> GetAllOrders()
        {
            return crudOrdre.GetAllOrders();
        }

        
    }
}
