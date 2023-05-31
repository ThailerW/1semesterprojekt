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
    internal class CRUD_Order
    {
        SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());

        /// <summary>
        /// Martin: Creates an order in the database, then returns the ID of the Order 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="orderDate"></param>
        /// <param name="totalPrice"></param>
        /// <returns></returns>
        public int CreateOrder(int customerID, DateTime orderDate, double totalPrice)
        {
            int orderID = 0;
            try
            {
                string query = "INSERT INTO SP_Ordre2 (KundeID, ordreDato, totalpris) " +
                "OUTPUT INSERTED.OrdreID " +
                "VALUES (@kundeID, @datetime, @totalPrice)";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@kundeID", customerID);
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

        /// <summary>
        /// Martin: Gets all orders as a list
        /// </summary>
        /// <returns></returns>
        public List<Models.Order> GetAllOrders()
        {
            List<Models.Order> allOrders = new List<Models.Order>();

            try
            {
                string query = "SELECT * FROM SP_Ordre2";
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int orderID = Convert.ToInt32(reader["OrdreID"]);
                    int customerID = Convert.ToInt32(reader["KundeID"]);
                    DateTime orderDate = Convert.ToDateTime(reader["ordreDato"]);
                    double totalPris = Convert.ToDouble(reader["totalPris"]);

                    Models.Order order = new Models.Order(orderID,customerID, orderDate, totalPris);
                    allOrders.Add(order);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved at få alle ordre. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
            return allOrders;
        }
    }
}
