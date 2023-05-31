using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class Supplier_Service
    {

        public void CreateSupplier(string name, string adress, int zipCode, string email, string billingInfo, int phoneNumber)
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            crudSupplier.CreateSupplier(name, adress, zipCode, email, billingInfo, phoneNumber);
        }

        public void ReadSupplier(string levId, out string levId2, out string name, out string adress, out string zipCode, out string email, 
            out string billingInfo, out string phoneNumber)
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            crudSupplier.ReadSupplier(levId, out levId2, out name, out adress, out zipCode, out email, out billingInfo, out phoneNumber);
        }

        public void UpdateSupplier(string levId, string name, string adress, int zipCode, string email, string billingInfo, int phoneNumber)
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            crudSupplier.UpdateSupplier(levId, name, adress, zipCode, email, billingInfo, phoneNumber);
        }

        public void DeleteSupplier(string supplierID)
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            crudSupplier.DeleteSupplier(supplierID);
        }

        public List<Models.Supplier> SearchSupplierByName(string name)
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            return crudSupplier.SearchSupplierByName(name);
        }

        public List<Models.Supplier> GetAllSupplier()
        {
            Database.CRUD_Supplier crudSupplier = new Database.CRUD_Supplier();
            var allSuppliers = crudSupplier.GetAllSuppliers();
            return allSuppliers;
        }

    }
}
