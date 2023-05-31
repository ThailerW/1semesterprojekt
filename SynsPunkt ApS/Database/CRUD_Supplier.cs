using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Database
{
    public class CRUD_Supplier
    {
        string connectionString = Database.ConnectionString.GetConnectionString();

        /// <summary>
        /// Theis: Creates a new supplier in the database with the given data.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adress"></param>
        /// <param name="email"></param>
        /// <param name="billingInfo"></param>
        /// <param name="phoneNumber"></param>
        public void CreateSupplier(string name, string adress, int zipCode, string email, string billingInfo, int phoneNumber)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.CommandText = "INSERT INTO SP_Leverandør2 (leverandørNavn, adresse, postNr, email, faktureringsOplysninger, telefonNummer) " +
                                      "VALUES ('" + name + "', '" + adress + "', " + zipCode + ", '" + email + "', '"
                                      + billingInfo + "', " + phoneNumber + ")";


                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex.Message, "OOPS!", MessageBoxButtons.OK);
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// Theis: Reads all data bound to the inputted ID
        /// </summary>
        /// <param name="levId"></param>
        /// <param name="levId2"></param>
        /// <param name="name"></param>
        /// <param name="adress"></param>
        /// <param name="zipCode"></param>
        /// <param name="email"></param>
        /// <param name="billingInfo"></param>
        /// <param name="phoneNumber"></param>
        public Models.Supplier ReadSupplier(string levId, out string levId2, out string name, out string adress, out string zipCode, out string email, 
            out string billingInfo, out string phoneNumber)
        {
            levId2 = "";
            name = "";
            adress = "";
            zipCode = "";
            email = "";
            billingInfo = "";
            phoneNumber = "";

            Models.Supplier supplier = new Models.Supplier(0, "", "", 0, "", "", 0);
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM SP_Leverandør2 WHERE cvrID = " + levId + ";";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = null;
            try
            {
                command.CommandText = query;

                connection.Open();
                reader = command.ExecuteReader();

                //While the reader is reading through all the rows, it adds the attributes to the instance of the class.
                while (reader.Read())
                {
                    //Filling created instance with the selected ID's data.
                    supplier = new Models.Supplier(Convert.ToInt32(reader["cvrID"]), reader["leverandørNavn"].ToString(), reader["adresse"].ToString(),
                       Convert.ToInt32(reader["postNr"]), reader["email"].ToString(), reader["faktureringsOplysninger"].ToString(), 
                       Convert.ToInt32(reader["telefonNummer"]));

                    levId2 = supplier.CVRnummer.ToString();
                    name = supplier.supplierName;
                    adress = supplier.adress;
                    zipCode = supplier.zipCode.ToString();
                    email = supplier.mail;
                    billingInfo = supplier.billingInformation;
                    phoneNumber = supplier.phoneNumber.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex.Message, "OOPS!", MessageBoxButtons.OK);
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
            return supplier;
        }

        /// <summary>
        /// Theis: Updates all data bound to the inputted ID
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
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.CommandText = "UPDATE SP_Leverandør2 SET leverandørNavn = '" + name + "', adresse = '" + adress +
                    "', postNr = " + zipCode + ", email = '" + email + "', faktureringsOplysninger = '" + billingInfo + "', telefonNummer = " + phoneNumber + " " +
                    "WHERE cvrID = " + levId + ";";


                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex.Message, "OOPS!", MessageBoxButtons.OK);
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// Theis: Deletees the supplier bound to the inputted ID
        /// </summary>
        /// <param name="levId"></param>
        public void DeleteSupplier(string levId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            try
            {
                command.CommandText = "DELETE FROM SP_Leverandør2 WHERE cvrID = '" + levId + "'";

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex.Message, "OOPS!", MessageBoxButtons.OK);
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// Theis: Returns a list of all suppliers.
        /// </summary>
        /// <returns></returns>
        public List<Models.Supplier> GetAllSuppliers()
        {
            List<Models.Supplier> SupplierList = new List<Models.Supplier>();

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            SqlDataReader reader = null;

            try
            {
                command.CommandText = "SELECT * FROM SP_Leverandør2";

                connection.Open();
                reader = command.ExecuteReader();

                //While the reader is reading through all the rows, it adds the attributes to the instance of the class.
                while (reader.Read())
                {
                    //Creating new instance of class:
                    Models.Supplier supplier = new Models.Supplier(
                       Convert.ToInt32(reader["cvrID"]),
                       reader["leverandørNavn"].ToString(),
                       reader["adresse"].ToString(),
                       Convert.ToInt32(reader["postNr"]),
                       reader["email"].ToString(),
                       reader["faktureringsOplysninger"].ToString(),
                       Convert.ToInt32(reader["telefonNummer"]));

                    SupplierList.Add(supplier);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex.Message, "OOPS!", MessageBoxButtons.OK);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.Close();
            }
            return SupplierList;
        }

        /// <summary>
        /// Theis: returns a list of suppliers where the input string is contained in the supplier name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Models.Supplier> SearchSupplierByName(string name)
        {
            List<Models.Supplier> searchResults = new List<Models.Supplier>();


            string query = "SELECT * FROM SP_Leverandør2 WHERE leverandørNavn LIKE '%' + @name + '%'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    int cvrID = Convert.ToInt32(reader["cvrID"]);
                    string supplierName = reader["leverandørNavn"].ToString();
                    string adress = reader["adresse"].ToString();
                    int zipCode = Convert.ToInt32(reader["postNr"]);
                    string email = reader["email"].ToString();
                    string billingInfo = reader["faktureringsOplysninger"].ToString();
                    int phoneNumber = Convert.ToInt32(reader["telefonNummer"]);


                    Models.Supplier leverandør = new Models.Supplier(cvrID, supplierName, adress, zipCode, email, billingInfo, phoneNumber);

                    searchResults.Add(leverandør);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved søgning af Leverandør. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                connection.Close();
            }

            return searchResults;
        }
    }
}
    





 