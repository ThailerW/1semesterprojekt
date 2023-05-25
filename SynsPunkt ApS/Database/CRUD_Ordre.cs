using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Database
{
    internal class CRUD_Ordre
    {
        SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());
        public int CreateOrdre(int kundeID, DateTime orderDate, double totalPrice)
        {
            int orderID = 0;
            try
            {
                string query = "INSERT INTO SP_Ordre (KundeID, ordreDato, totalpris) " +
                "OUTPUT INSERTED.OrdreID " +
                "VALUES (@kundeID, @datetime, @totalPrice)";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@kundeID", kundeID);
                command.Parameters.AddWithValue("@datetime", orderDate);
                command.Parameters.AddWithValue("@totalPrice", totalPrice);

                conn.Open();
                orderID = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved oprettelse af Ordre. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
            return orderID;
        }
    }
}
