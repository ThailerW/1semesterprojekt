using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SynsPunkt_ApS.Database
{
    public class CRUD_Product
    {
        string connectionString = Database.ConnectionString.GetConnectionString();

        /// <summary>
        /// Martin: Opretter en ny vare med alt tildelt info
        /// </summary>
        /// <param name="productDescription"></param>
        /// <param name="stockQuantity"></param>
        /// <param name="productName"></param>
        /// <param name="lensStrength"></param>
        /// <param name="levCVR"></param>
        public void CreateProduct(string productDescription, int stockQuantity, string productName, decimal lensStrength, string levCVR, decimal price)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.CommandText = "INSERT INTO SP_Vare (vareBeskrivelse, lagerMængde, vareNavn, styrke, leverandørCVR, varePris) " +
                                      "VALUES ('" + productDescription + "', " + stockQuantity + ", '" + productName + "', "
                                      + lensStrength + ",'" + levCVR + "', " + price + ")";


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
        /// Læser al info knyttet til det indtastede vareID
        /// </summary>
        /// <param name="vareID"></param>
        /// <returns></returns>
        public Models.Product ReadProduct(string id, out string id2, out string productDescription, out string stockQuantity, out string productName,
            out string lensStrength, out string levCVR, out string price)
        {
            id2 = "";
            productDescription = "";
            stockQuantity = "";
            productName = "";
            lensStrength = "";
            levCVR = "";
            price = "";

            Models.Product product = new Models.Product(0, "", 0, "", 0, "", 0);
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM SP_Vare WHERE vareID = '" + id + "'";

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
                    product = new Models.Product(Convert.ToInt32(reader["vareID"]), reader["vareBeskrivelse"].ToString(),
                       Convert.ToInt32(reader["lagerMængde"]), reader["vareNavn"].ToString(), Convert.ToDecimal(reader["styrke"]),
                       reader["leverandørCVR"].ToString(), Convert.ToDecimal(reader["varePris"]));

                    id2 = product.productNumber.ToString();
                    productDescription = product.productDescription;
                    stockQuantity = product.stockQuantity.ToString();
                    productName = product.productName;
                    lensStrength = product.lensStrength.ToString();
                    levCVR = product.levCVR.ToString();
                    price = product.price.ToString();
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
            return product;
        }

        /// <summary>
        /// Opdatere en vare med valgt VareID med ny anvgivet data.
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="prodcutDescription"></param>
        /// <param name="stockQuantity"></param>
        /// <param name="productName"></param>
        /// <param name="lensStrength"></param>
        /// <param name="levCVR"></param>
        public void UpdateProduct(string productID, string prodcutDescription, int stockQuantity, string productName, decimal lensStrength, string levCVR, decimal pris)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.CommandText = "UPDATE SP_Vare SET vareBeskrivelse = '" + prodcutDescription + "', lagerMængde = " + stockQuantity +
                    ", vareNavn = '" + productName + "', styrke = " + lensStrength + ", leverandørCVR = " + levCVR + ", varePris = " + pris + " " +
                    "WHERE vareID = " + productID + ";";


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
        /// Sletter valgt vare med inputted ID
        /// </summary>
        /// <param name="productID"></param>
        public void DeleteProduct(string productID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            try
            {
                command.CommandText = "DELETE FROM SP_Vare WHERE vareID = '" + productID + "'";

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
        /// Henter alle vare fra databasen og returnere en liste af models.vare
        /// </summary>
        /// <returns></returns>
        public List<Models.Product> GetAllProducts()
        {
            List<Models.Product> productList = new List<Models.Product>();

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            SqlDataReader reader = null;

            try
            {
                command.CommandText = "SELECT * FROM SP_Vare";

                connection.Open();
                reader = command.ExecuteReader();

                //While the reader is reading through all the rows, it adds the attributes to the instance of the class.
                while (reader.Read())
                {
                    //Creating new instance of class:
                    Models.Product product = new Models.Product(
                       Convert.ToInt32(reader["vareID"]),
                       reader["vareBeskrivelse"].ToString(),
                       Convert.ToInt32(reader["lagerMængde"]),
                       reader["vareNavn"].ToString(),
                       Convert.ToDecimal(reader["styrke"]),
                       reader["leverandørCVR"].ToString(),
                       Convert.ToDecimal(reader["varePris"]));

                    productList.Add(product);
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
            return productList;
        }

        public List<Models.Product> SearchProductByName(string name)
        {
            List<Models.Product> searchResults = new List<Models.Product>();


            string query = "SELECT * FROM SP_Vare WHERE vareNavn LIKE '%' + @name + '%'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    int productID = Convert.ToInt32(reader["VareID"]);
                    string productDescription = reader["vareBeskrivelse"].ToString();
                    int stockQuantity = Convert.ToInt32(reader["lagerMængde"]);
                    string productName = reader["vareNavn"].ToString();
                    decimal lensStrength = Convert.ToDecimal(reader["styrke"]);
                    string levCVR = reader["leverandørCVR"].ToString();
                    decimal productPrice = Convert.ToDecimal(reader["varePris"]);


                    Models.Product Vare = new Models.Product(productID, productDescription, stockQuantity, productName, lensStrength, levCVR, productPrice);

                    searchResults.Add(Vare);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved søgning af Vare. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                connection.Close();
            }

            return searchResults;
        }
    }
}
