using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    internal class ProductLine_service
    {
        private Database.CRUD_ProductLine crudProductLine = new Database.CRUD_ProductLine();

        /// <summary>
        /// Martin: Takes inputs, uses database method to create a product line in the database
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="orderID"></param>
        /// <param name="quantity"></param>
        public void CreateProductLine(int productID, int orderID, int quantity)
        {
            crudProductLine.CreateProductLine(productID, orderID, quantity);
        }

        /// <summary>
        /// Martin:Takes inputs, uses database method to update product line in the database (This method is currently not used anywhere)
        /// </summary>
        /// <param name="productLineID"></param>
        /// <param name="orderID"></param>
        /// <param name="quantity"></param>
        public void UpdateProductLine(int productLineID, int orderID, int quantity)
        {
            crudProductLine.UpdateProductLine(productLineID, orderID, quantity);
        }

        /// <summary>
        /// Martin: Takes a single input, uses database method to delete a product line in the database (This method is not currently used anywhere)
        /// </summary>
        /// <param name="productLineID"></param>
        public void DeleteProductLine(int productLineID)
        {
            crudProductLine.DeleteProductLine(productLineID);

        }

    }
}
