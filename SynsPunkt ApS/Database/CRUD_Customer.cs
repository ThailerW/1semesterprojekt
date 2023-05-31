using SynsPunkt_ApS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Database
{
    public class CRUD_Customer
    {
        SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());

        private string connectionString = "Data Source=mssql15.unoeuro.com;Initial Catalog=oversaftigt_dk_db_test;User ID=oversaftigt_dk;Password=prfHFR9546nEtyb2xDmc";

        /// <summary>
        /// Opretter en ny kunde i databasen.
        /// </summary>
        /// <param name="nyKunde">Den nye kunde, der skal oprettes.</param>
        public void CreateCustomer(string locationId, string Mail, string firstName, string lastName, int phoneNumber, string adress, int zipCode)
        {
            try
            {
                string query = "INSERT INTO SP_Kunde " +
                "(LokationID, Mail, Fornavn, Efternavn, TelefonNummer, Adresse, PostNr) " +
                "VALUES ((SELECT LokationID FROM SP_Optiker WHERE byNavn = @Lokation), @Mail, @Fornavn, @Efternavn, @TelefonNummer, @Adresse, @PostNr)";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Lokation", locationId);
                command.Parameters.AddWithValue("@Fornavn", firstName);
                command.Parameters.AddWithValue("@Efternavn", lastName);
                command.Parameters.AddWithValue("@TelefonNummer", phoneNumber.ToString()); // Convert to string
                command.Parameters.AddWithValue("@Mail", Mail);
                command.Parameters.AddWithValue("@Adresse", adress);
                command.Parameters.AddWithValue("@PostNr", zipCode);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved oprettelse af Kunde. " + ex.Message, "Der opstod en fejl", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// Henter en kunde fra databasen baseret på KundeInfo-id.
        /// </summary>
        /// <param name="KundeInfo">Id'et på den ønskede kunde.</param>
        public Customer GetCustomer(int customerID)
        {
            string query = "SELECT * FROM SP_Kunde WHERE KundeID = @KundeID";
            Customer customer = null;

            SqlCommand command = new SqlCommand(query, conn);
            {
                conn.Open();

                {
                    command.Parameters.AddWithValue("@KundeId", customerID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customer(
                                reader["Fornavn"].ToString(),
                                reader["Efternavn"].ToString(),
                                Convert.ToInt32(reader["TelefonNummer"]),
                                reader["Mail"].ToString(),
                                reader["Adresse"].ToString(),
                                reader["KundeID"].ToString(),
                                Convert.ToInt32(reader["PostNr"]),
                                reader["LokationID"].ToString()
                            );
                        }
                    }
                }
                conn.Close();
            }

            return customer;
        }



        /// <summary>
        /// Opdaterer en kunde i databasen.
        /// </summary>
        public void UpdateCustomer(string locationID, int customerID, string Mail, string firstName, string lastName, int phoneNumber, string adress, int zipCode)
        {
            try
            {
                string query = "UPDATE SP_Kunde SET LokationID = (SELECT LokationID FROM SP_Optiker WHERE byNavn = @LokationID), Mail = @Mail, Fornavn = @Fornavn, Efternavn = @Efternavn, TelefonNummer = @TelefonNummer, Adresse = @Adresse, PostNr = @PostNr WHERE KundeID = @KundeID";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@LokationID", locationID);
                command.Parameters.AddWithValue("@Mail", Mail);
                command.Parameters.AddWithValue("@Fornavn", firstName);
                command.Parameters.AddWithValue("@Efternavn", lastName);
                command.Parameters.AddWithValue("@TelefonNummer", phoneNumber);
                command.Parameters.AddWithValue("@Adresse", adress);
                command.Parameters.AddWithValue("@PostNr", zipCode);
                command.Parameters.AddWithValue("@KundeID", customerID);

                // Konverter lokationId til en integer
                int lokationIdInt;
                if (int.TryParse(locationID, out lokationIdInt))
                {
                    command.Parameters.AddWithValue("@LokationID", lokationIdInt);
                }
                else
                {
                    // Håndter fejl, hvis konverteringen mislykkes
                    // Du kan vise en fejlbesked eller foretage andre handlinger her
                }



                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved opdatering af kunde: " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
        }



        /// <summary>
        /// Sletter en kunde fra databasen baseret på Id.
        /// </summary>
        /// <param name="id">Id'et på kunden, der skal slettes.</param>

        public void DeleteCustomer(int customerID)
        {
            try
            {
                string query = "DELETE FROM SP_Kunde WHERE KundeID = @KundeID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@KundeID", customerID);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved sletning af Kunde" + ex.Message, "Fejl", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// Udfører en generel databaseforespørgsel.
        /// </summary>
        /// <param name="query">SQL-forespørgslen, der skal udføres.</param>
        private void ExecuteQuery(string query)
        {
            SqlCommand command = new SqlCommand(query, conn);
            {
                conn.Open();
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Customer> FindCustomerInfo(string customerName)
        {
            string query = "SELECT * FROM Kunder WHERE Navn LIKE @KundeNavn";
            List<Customer> customerList = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KundeNavn", "%" + customerName + "%");

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer(
                                reader["Fornavn"].ToString(),
                                reader["Efternavn"].ToString(),
                                Convert.ToInt32(reader["TelefonNummer"]),
                                reader["Mail"].ToString(),
                                reader["Adresse"].ToString(),
                                reader["KundeID"].ToString(),
                                Convert.ToInt32(reader["PostNr"]),
                                reader["LokationID"].ToString()
                            );

                            customerList.Add(customer);
                        }
                    }

                    reader.Close();
                }
            }

            return customerList;
        }

        public List<Customer> GetCustomers()
        {
            string connectionString = "Data Source=mssql15.unoeuro.com;Initial Catalog=oversaftigt_dk_db_test;User ID=oversaftigt_dk;Password=prfHFR9546nEtyb2xDmc";

            string query = "SELECT * FROM SP_Kunde";
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Customer kunde = new Customer(
                                reader["Fornavn"].ToString(),
                                reader["Efternavn"].ToString(),
                                Convert.ToInt32(reader["TelefonNummer"]),
                                reader["Mail"].ToString(),
                                reader["Adresse"].ToString(),
                                reader["KundeID"].ToString(),
                                Convert.ToInt32(reader["PostNr"]),
                                reader["LokationID"].ToString()
                            );

                            customers.Add(kunde);
                        }
                    }

                    reader.Close();
                }
            }

            return customers;
        }

        /// <summary>
        /// Martin: Checks if a customer exists in the database based on the ID
        /// </summary>
        /// <param name="kundeID"></param>
        /// <returns></returns>
        public bool CheckIfCustomerExists(int kundeID)
        {
            bool customerExists = false;
            string query = "SELECT COUNT(*) FROM SP_Kunde WHERE KundeID = @kundeID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kundeID", kundeID);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        customerExists = true;
                    }
                }
            }
            return customerExists;
        }
    }
}












