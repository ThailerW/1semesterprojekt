using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    internal class Varelinje_service
    {
        private Database.CRUD_Varelinje crudVarelinje = new Database.CRUD_Varelinje();

        public void CreateVarelinje(int vareID, int ordreID, int quantity)
        {
            crudVarelinje.CreateVarelinje(vareID, ordreID, quantity);
        }

        public void UpdateVarelinje(int varelinjeID, int ordreID, int quantity)
        {
            crudVarelinje.UpdateVarelinje(varelinjeID, ordreID, quantity);
        }

        public void DeleteVarelinje(int varelinjeID)
        {
            crudVarelinje.DeleteVarelinje(varelinjeID);

        }

    }
}
