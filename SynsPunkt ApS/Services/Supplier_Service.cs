using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class Supplier_Service
    {
        /// <summary>
        /// Theis: Creates a supplier in the database with the given data.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adress"></param>
        /// <param name="zipCode"></param>
        /// <param name="email"></param>
        /// <param name="billingInfo"></param>
        /// <param name="phoneNumber"></param>
        public void CreateSupplier(string name, string adress, int zipCode, string email, string billingInfo, int phoneNumber)
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            crudSupplier.CreateSupplier(name, adress, zipCode, email, billingInfo, phoneNumber);
        }

        /// <summary>
        /// Theis: Reads and outs all the attributes matching the given ID.
        /// </summary>
        /// <param name="levId"></param>
        /// <param name="levId2"></param>
        /// <param name="name"></param>
        /// <param name="adress"></param>
        /// <param name="zipCode"></param>
        /// <param name="email"></param>
        /// <param name="billingInfo"></param>
        /// <param name="phoneNumber"></param>
        public void ReadSupplier(string levId, out string levId2, out string name, out string adress, out string zipCode, out string email, 
            out string billingInfo, out string phoneNumber)
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            crudSupplier.ReadSupplier(levId, out levId2, out name, out adress, out zipCode, out email, out billingInfo, out phoneNumber);
        }

        /// <summary>
        /// Theis: Updates the supplier with the inputted ID, with the new inputted data.
        /// </summary>
        /// <param name="levId"></param>
        /// <param name="name"></param>
        /// <param name="adress"></param>
        /// <param name="zipCode"></param>
        /// <param name="email"></param>
        /// <param name="billingInfo"></param>
        /// <param name="phoneNumber"></param>
        public void UpdateSupplier(string levId, string name, string adress, int zipCode, string email, string billingInfo, int phoneNumber)
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            crudSupplier.UpdateSupplier(levId, name, adress, zipCode, email, billingInfo, phoneNumber);
        }

        /// <summary>
        /// Theis: Deletes the supplier with the inputted ID from the database.
        /// </summary>
        /// <param name="supplierID"></param>
        public void DeleteSupplier(string supplierID)
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            crudSupplier.DeleteSupplier(supplierID);
        }

        /// <summary>
        /// Theis: Searches the database for a supplier with the inputted name, and returns them in a list.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Models.Supplier> SearchSupplierByName(string name)
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            return crudSupplier.SearchSupplierByName(name);
        }

        /// <summary>
        /// Theis: Returns a list of all suppliers in the database.
        /// </summary>
        /// <returns></returns>
        public List<Models.Supplier> GetAllSupplier()
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            var allSuppliers = crudSupplier.GetAllSuppliers();
            return allSuppliers;
        }

    }
}
