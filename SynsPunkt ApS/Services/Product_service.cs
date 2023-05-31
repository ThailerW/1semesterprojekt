using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class Product_service
    {
        /// <summary>
        /// Theis: Creates a product in the database with the given inputs as attributes.
        /// </summary>
        /// <param name="productDescription"></param>
        /// <param name="stockQuantity"></param>
        /// <param name="productName"></param>
        /// <param name="lensStrength"></param>
        /// <param name="levCVR"></param>
        /// <param name="price"></param>
        public void CreateProduct(string productDescription, int stockQuantity, string productName, decimal lensStrength, string levCVR, decimal price)
        {
            Database.CRUD_Product crudProduct = new Database.CRUD_Product();
            crudProduct.CreateProduct(productDescription, stockQuantity, productName, lensStrength, levCVR, price);
        }

        /// <summary>
        /// Theis: Reads and returns all the attributes of a given ProductID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        /// <param name="productDescription"></param>
        /// <param name="stockQuantity"></param>
        /// <param name="productName"></param>
        /// <param name="lensStrength"></param>
        /// <param name="levCVR"></param>
        /// <param name="price"></param>
        public void ReadProduct(string id, out string id2, out string productDescription, out string stockQuantity, out string productName,
            out string lensStrength, out string levCVR, out string price)
        {
            Database.CRUD_Product crudProduct = new Database.CRUD_Product();
            crudProduct.ReadProduct(id, out id2, out productDescription, out stockQuantity, out productName, out lensStrength, out levCVR, out price);
        }

        /// <summary>
        /// Theis: Updates a product with the inputted productID with the given inputs.
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="productDescription"></param>
        /// <param name="stockQuantity"></param>
        /// <param name="productName"></param>
        /// <param name="lensStength"></param>
        /// <param name="levCVR"></param>
        /// <param name="price"></param>
        public void UpdateProduct(string productID, string productDescription, int stockQuantity, string productName, decimal lensStength, string levCVR, decimal price)
        {
            Database.CRUD_Product crudProduct = new Database.CRUD_Product();
            crudProduct.UpdateProduct(productID, productDescription, stockQuantity, productName, lensStength, levCVR, price);
        }

        /// <summary>
        /// Theis: Deletes the product with the inputted ID from the database.
        /// </summary>
        /// <param name="productID"></param>
        public void DeleteProduct(string productID)
        {
            Database.CRUD_Product crudVare = new Database.CRUD_Product();
            crudVare.DeleteProduct(productID);
        }

        /// <summary>
        /// Theis: Searches the database for a product with the same name as the input, and returns them in a list.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Models.Product> SearchProductByName(string name)
        {
            Database.CRUD_Product crudProduct = new Database.CRUD_Product();
            return crudProduct.SearchProductByName(name);
        }

        /// <summary>
        /// Theis: Returns a list of all products in the database.
        /// </summary>
        /// <returns></returns>
        public List<Models.Product> GetAllProduct()
        {
            Database.CRUD_Product crudProduct = new Database.CRUD_Product();
            var allProducts = crudProduct.GetAllProducts();
            return allProducts;
        }
    }
}
