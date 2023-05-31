using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class Product_service
    {
        public void CreateProduct(string productDescription, int stockQuantity, string productName, decimal lensStrength, string levCVR, decimal price)
        {
            Database.CRUD_Product crudProduct = new Database.CRUD_Product();
            crudProduct.CreateProduct(productDescription, stockQuantity, productName, lensStrength, levCVR, price);
        }

        public void ReadProduct(string id, out string id2, out string productDescription, out string stockQuantity, out string productName,
            out string lensStrength, out string levCVR, out string price)
        {
            Database.CRUD_Product crudProduct = new Database.CRUD_Product();
            crudProduct.ReadProduct(id, out id2, out productDescription, out stockQuantity, out productName, out lensStrength, out levCVR, out price);
        }

        public void UpdateProduct(string productID, string productDescription, int stockQuantity, string productName, decimal lensStength, string levCVR, decimal price)
        {
            Database.CRUD_Product crudProduct = new Database.CRUD_Product();
            crudProduct.UpdateProduct(productID, productDescription, stockQuantity, productName, lensStength, levCVR, price);
        }

        public void DeleteProduct(string productID)
        {
            Database.CRUD_Product crudVare = new Database.CRUD_Product();
            crudVare.DeleteProduct(productID);
        }

        public List<Models.Product> SearchProductByName(string name)
        {
            Database.CRUD_Product crudProduct = new Database.CRUD_Product();
            return crudProduct.SearchProductByName(name);
        }

        public List<Models.Product> GetAllProduct()
        {
            Database.CRUD_Product crudProduct = new Database.CRUD_Product();
            var allProducts = crudProduct.GetAllProducts();
            return allProducts;
        }
    }
}
