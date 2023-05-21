using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Database
{
    public class CRUD_Leverandør
    {
      
        public void CreateLeverandør()
        {
            // Logik
        }

        public void ReadLeverandør()
        {
            // Logik
        }

        public void UpdateLeverandør()
        {
            // Logik
        }

        public void DeleteLeverandør()
        {
            // Logik
        }

        private string connectionString = "Data Source=mssql15.unoeuro.com;Initial Catalog=oversaftigt_dk_db_test;User ID=oversaftigt_dk;Password=prfHFR9546nEtyb2xDmc";

        public CRUD_Leverandør(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Opretter en leverandør i databasen.
        /// </summary>
        public void TilføjLeverandør(string leverandørnavn, int telefonNummer, string adresse, string fakturaOplysninger)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Leverandør (Leverandørnavn, TelefonNummer, Adresse, FakturaOplysninger) VALUES (@navn, @telefon, @adresse, @faktura)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@navn", leverandørnavn);
                command.Parameters.AddWithValue("@telefon", telefonNummer);
                command.Parameters.AddWithValue("@adresse", adresse);
                command.Parameters.AddWithValue("@faktura", fakturaOplysninger);
                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Opdaterer en leverandør i databasen.
        /// </summary>
        public void OpdaterLeverandør(int leverandørId, string leverandørnavn, int telefonNummer, string adresse, string fakturaOplysninger)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Leverandør SET Leverandørnavn = @navn, TelefonNummer = @telefon, Adresse = @adresse, FakturaOplysninger = @faktura WHERE LeverandørID = @id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@id", leverandørId);
                command.Parameters.AddWithValue("@navn", leverandørnavn);
                command.Parameters.AddWithValue("@telefon", telefonNummer);
                command.Parameters.AddWithValue("@adresse", adresse);
                command.Parameters.AddWithValue("@faktura", fakturaOplysninger);
                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Sletter en leverandør fra databasen.
        /// </summary>
        public void SletLeverandør(int leverandørId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Leverandør WHERE LeverandørID = @id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@id", leverandørId);
                conn.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
    





 