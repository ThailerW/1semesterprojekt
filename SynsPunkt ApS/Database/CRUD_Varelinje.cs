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
    internal class CRUD_Varelinje
    {
        SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());

        public void CreateVarelinje(int vareID, int ordreID, int quantity)
        {
            try
            {
                string query = "INSERT INTO SP_Varelinje VALUES (@vareID, @ordreID, @quantity, @quantity * (SELECT varePris FROM SP_Vare WHERE vareID = @vareID))";

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@vareID", vareID);
                command.Parameters.AddWithValue("@ordreID", ordreID); //DBNULL.Value = NULL
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

        public void UpdateVarelinje(int varelinjeID, int ordreID, int quantity)
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

                command.Parameters.AddWithValue("@varelinjeID", varelinjeID);
                command.Parameters.AddWithValue("@ordreID", ordreID);
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

        public void DeleteVarelinje(int varelinjeID)
        {
            try
            {
                string query = "DELETE FROM SP_Varelinje WHERE varelinjeID = @varelinjeID";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@varelinjeID", varelinjeID);

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
