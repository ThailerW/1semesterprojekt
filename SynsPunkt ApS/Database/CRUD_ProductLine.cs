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
    internal class CRUD_ProductLine
    {
        SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());

        /// <summary>
        /// Martin: Creates a product line in the database
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="orderID"></param>
        /// <param name="quantity"></param>
        public void CreateProductLine(int productID, int orderID, int quantity)
        {
            try
            {
                string query = "INSERT INTO SP_Varelinje VALUES (@vareID, @ordreID, @quantity, @quantity * (SELECT varePris FROM SP_Vare WHERE vareID = @vareID))";

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@vareID", productID);
                command.Parameters.AddWithValue("@ordreID", orderID); //DBNULL.Value = NULL
                command.Parameters.AddWithValue("@quantity", quantity);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved oprettelse af Varelinje. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Martin: Updates a product line in the database
        /// </summary>
        /// <param name="productLineID"></param>
        /// <param name="orderID"></param>
        /// <param name="quantity"></param>
        public void UpdateProductLine(int productLineID, int orderID, int quantity)
        {
            try
            {
                string query = "UPDATE SP_Varelinje " +
                    "SET " +
                    "ordreID = @ordreID," +
                    "mængde = mængde + @quantity," +
                    "totalPris = totalPris + (@quantity * (SELECT varePris FROM SP_Vare WHERE vareID = @vareID))" +
                    "WHERE varelinjeID = @varelinjeID";

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@varelinjeID", productLineID);
                command.Parameters.AddWithValue("@ordreID", orderID);
                command.Parameters.AddWithValue("@quantity", quantity);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved opdatering af Varelinje. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Martin: Deletes a product line from the database
        /// </summary>
        /// <param name="productLineID"></param>
        public void DeleteProductLine(int productLineID)
        {
            try
            {
                string query = "DELETE FROM SP_Varelinje WHERE varelinjeID = @varelinjeID";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@varelinjeID", productLineID);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved sletning af Varelinje. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }

        }

    }
}
