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
    public class CRUD_Kunde
    {
        SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());

        private string connectionString = "Data Source=mssql15.unoeuro.com;Initial Catalog=oversaftigt_dk_db_test;User ID=oversaftigt_dk;Password=prfHFR9546nEtyb2xDmc";

        /// <summary>
        /// Opretter en ny kunde i databasen.
        /// </summary>
        /// <param name="nyKunde">Den nye kunde, der skal oprettes.</param>
        public void CreateKunde(string lokationId, string Mail, string forNavn, string efterNavn, int telefonNummer, string adresse, int postNr)
        {
            try
            {
                string query = "INSERT INTO SP_Kunde " +
                "(LokationID, Mail, Fornavn, Efternavn, TelefonNummer, Adresse, PostNr) " +
                "VALUES ((SELECT LokationID FROM SP_Optiker WHERE byNavn = @Lokation), @Mail, @Fornavn, @Efternavn, @TelefonNummer, @Adresse, @PostNr)";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Lokation", lokationId);
                command.Parameters.AddWithValue("@Fornavn", forNavn);
                command.Parameters.AddWithValue("@Efternavn", efterNavn);
                command.Parameters.AddWithValue("@TelefonNummer", telefonNummer.ToString()); // Convert to string
                command.Parameters.AddWithValue("@Mail", Mail);
                command.Parameters.AddWithValue("@Adresse", adresse);
                command.Parameters.AddWithValue("@PostNr", postNr);

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
        public Customer HentKunde(int KundeID)
        {
            string query = "SELECT * FROM SP_Kunde WHERE KundeID = @KundeID";
            Customer kunde = null;

            SqlCommand command = new SqlCommand(query, conn);
            {
                conn.Open();

                {
                    command.Parameters.AddWithValue("@KundeId", KundeID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            kunde = new Customer(
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

            return kunde;
        }



        /// <summary>
        /// Opdaterer en kunde i databasen.
        /// </summary>
        public void UpdateKunde(string lokationId, int KundeID, string Mail, string forNavn, string efterNavn, int telefonNummer, string adresse, int postNr)
        {
            try
            {
                string query = "UPDATE SP_Kunde SET LokationID = (SELECT LokationID FROM SP_Optiker WHERE byNavn = @LokationID), Mail = @Mail, Fornavn = @Fornavn, Efternavn = @Efternavn, TelefonNummer = @TelefonNummer, Adresse = @Adresse, PostNr = @PostNr WHERE KundeID = @KundeID";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@LokationID", lokationId);
                command.Parameters.AddWithValue("@Mail", Mail);
                command.Parameters.AddWithValue("@Fornavn", forNavn);
                command.Parameters.AddWithValue("@Efternavn", efterNavn);
                command.Parameters.AddWithValue("@TelefonNummer", telefonNummer);
                command.Parameters.AddWithValue("@Adresse", adresse);
                command.Parameters.AddWithValue("@PostNr", postNr);
                command.Parameters.AddWithValue("@KundeID", KundeID);

                // Konverter lokationId til en integer
                int lokationIdInt;
                if (int.TryParse(lokationId, out lokationIdInt))
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

        public void DeleteKunde(int KundeID)
        {
            try
            {
                string query = "DELETE FROM SP_Kunde WHERE KundeID = @KundeID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@KundeID", KundeID);
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

        public List<Customer> FindKundeInfo(string kundeNavn)
        {
            string query = "SELECT * FROM Kunder WHERE Navn LIKE @KundeNavn";
            List<Customer> kundeListe = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KundeNavn", "%" + kundeNavn + "%");

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

                            kundeListe.Add(kunde);
                        }
                    }

                    reader.Close();
                }
            }

            return kundeListe;
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












