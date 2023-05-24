using SynsPunkt_ApS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Database
{
    public class CRUD_Kunde
    {
        public void CreateKunde(Kunde kunde)
        {
            // Logik
        }
        public void ReadKunde()
        {
            // Logik
        }
        public void UpdateKunde(Kunde kunde)
        {
            // Logik
        }
        public void DeleteKunde(string kundeNummer)
        {
            // Logik
        }
        private string connectionString = "Data Source=mssql15.unoeuro.com;Initial Catalog=oversaftigt_dk_db_test;User ID=oversaftigt_dk;Password=prfHFR9546nEtyb2xDmc";

        /// <summary>
        /// Opretter en ny kunde i databasen.
        /// </summary>
        /// <param name="nyKunde">Den nye kunde, der skal oprettes.</param>
        public void OpretKunde(string fornavn, string efternavn, int telefonNummer, string privatEmail, string adresse,
            string kundeNummer, string kundeInfo, int postNr, Kunde nyKunde)
        {
            string query = "INSERT INTO Kunder (Fornavn, Efternavn, TelefonNummer, PrivatEmail, Adresse, KundeNummer, KundeInfo) " +
                           "VALUES (@Fornavn, @Efternavn, @TelefonNummer, @PrivatEmail, @Adresse, @KundeNummer, @KundeInfo)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fornavn", nyKunde.Fornavn);
                    command.Parameters.AddWithValue("@Efternavn", nyKunde.Efternavn);
                    command.Parameters.AddWithValue("@TelefonNummer", nyKunde.TelefonNummer);
                    command.Parameters.AddWithValue("@PrivatEmail", nyKunde.PrivatEmail);
                    command.Parameters.AddWithValue("@Adresse", nyKunde.Adresse);
                    command.Parameters.AddWithValue("@KundeNummer", nyKunde.KundeNummer);
                    command.Parameters.AddWithValue("@KundeInfo", nyKunde.KundeInfo);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Kunden blev oprettet");
        }

        /// <summary>
        /// Henter en kunde fra databasen baseret på KundeInfo-id.
        /// </summary>
        /// <param name="KundeInfo">Id'et på den ønskede kunde.</param>
        public Kunde HentKundeMedInfo(int KundeInfo)
        {
            string query = "SELECT * FROM Kunder WHERE Id = @KundeInfo";
            Kunde kunde = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KundeInfo", KundeInfo);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            kunde = new Kunde
                            (
                                reader["Fornavn"].ToString(),
                                reader["Efternavn"].ToString(),
                                Convert.ToInt32(reader["TelefonNummer"]),
                                reader["PrivatEmail"].ToString(),
                                reader["Adresse"].ToString(),
                                reader["KundeNummer"].ToString(),
                                reader["KundeInfo"].ToString(),
                                Convert.ToInt32(reader["postNr"])
                            );
                        }
                    }

                    reader.Close();
                }
            }

            return kunde;
        }



        /// <summary>
        /// Opdaterer en kunde i databasen.
        /// </summary>
        /// <param name="opdateretKunde">Den opdaterede kundeoplysninger.</param>
        public void OpdaterKunde(Kunde opdateretKunde)
        {
            string query = "UPDATE Kunder SET Fornavn = @Fornavn, Efternavn = @Efternavn, PrivatEmail = @PrivatEmail, " +
                           "TelefonNummer = @TelefonNummer, Adresse = @Adresse WHERE Id = @KundeInfo";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fornavn", opdateretKunde.Fornavn);
                    command.Parameters.AddWithValue("@Efternavn", opdateretKunde.Efternavn);
                    command.Parameters.AddWithValue("@PrivatEmail", opdateretKunde.PrivatEmail);
                    command.Parameters.AddWithValue("@TelefonNummer", opdateretKunde.KundeNummer);
                    command.Parameters.AddWithValue("@Adresse", opdateretKunde.Adresse);
                    command.Parameters.AddWithValue("@KundeInfo", opdateretKunde.KundeInfo.ToString());
                    command.Parameters.AddWithValue("PostNr", opdateretKunde.KundeInfo);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Kunde blev opdateret!");
        }

        /// <summary>
        /// Sletter en kunde fra databasen baseret på Id.
        /// </summary>
        /// <param name="id">Id'et på kunden, der skal slettes.</param>
        public void SletKunde(int id)
        {
            string query = "DELETE FROM Kunder WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Kunde blev slettet");
        }

        /// <summary>
        /// Udfører en generel databaseforespørgsel.
        /// </summary>
        /// <param name="query">SQL-forespørgslen, der skal udføres.</param>
        private void ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Kunde> FindKundeInfo(string kundeNavn)
        {
            string query = "SELECT * FROM Kunder WHERE Navn LIKE @KundeNavn";
            List<Kunde> kundeListe = new List<Kunde>();

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
                            Kunde kunde = new Kunde(
                                reader["Fornavn"].ToString(),
                                reader["Efternavn"].ToString(),
                                Convert.ToInt32(reader["TelefonNummer"]),
                                reader["PrivatEmail"].ToString(),
                                reader["Adresse"].ToString(),
                                reader["KundeNummer"].ToString(),
                                reader["KundeInfo"].ToString(),
                                Convert.ToInt32(reader["PostNr"])
                            );

                            kundeListe.Add(kunde);
                        }
                    }

                    reader.Close();
                }
            }

            return kundeListe;
        }
    }
}


