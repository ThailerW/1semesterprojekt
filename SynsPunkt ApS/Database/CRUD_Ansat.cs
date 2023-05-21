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
    public class CRUD_Ansat
    {
        /// <summary>
        /// Martin: Opretter en Ansat i databasen
        /// </summary>
        /// <param name="fornavn"></param>
        /// <param name="efternavn"></param>
        /// <param name="telefonNummer"></param>
        /// <param name="privatMail"></param>
        /// <param name="adresse"></param>
        /// <param name="adgangskode"></param>
        /// <param name="afdeling"></param>
        /// <param name="rolle"></param>
        /// <param name="arbejdsMail"></param>
        /// <param name="postNr"></param>
        public void CreateAnsat(string fornavn, string efternavn, int telefonNummer, string privatMail, string adresse,
        string adgangskode, string afdeling, int rolle, string arbejdsMail, int postNr)
        {
            SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());
            try
            {
                string query = "INSERT INTO SP_Ansat " +
                    "VALUES (@afdeling, @arbejdsMail, @fornavn, @efternavn, @telefonNummer, @adresse, @postNr, @rolle, @password, @privatMail)";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@afdeling", afdeling);
                command.Parameters.AddWithValue("@arbejdsMail", arbejdsMail);
                command.Parameters.AddWithValue("@fornavn", fornavn);
                command.Parameters.AddWithValue("@efternavn", efternavn);
                command.Parameters.AddWithValue("@telefonNummer", telefonNummer);
                command.Parameters.AddWithValue("@adresse", adresse);
                command.Parameters.AddWithValue("@postNr", postNr);
                command.Parameters.AddWithValue("@rolle", rolle);
                command.Parameters.AddWithValue("@password", adgangskode);
                command.Parameters.AddWithValue("@privatMail", privatMail);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved oprettelse af Ansat" + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }

        }
        
        /// <summary>
        /// Martin: Opdater en Ansats attributter
        /// </summary>
        /// <param name="medarbejderNummer"></param>
        /// <param name="fornavn"></param>
        /// <param name="efternavn"></param>
        /// <param name="telefonNummer"></param>
        /// <param name="privatMail"></param>
        /// <param name="adresse"></param>
        /// <param name="adgangskode"></param>
        /// <param name="afdeling"></param>
        /// <param name="rolle"></param>
        /// <param name="arbejdsMail"></param>
        /// <param name="postNr"></param>
        public void UpdateAnsat(int medarbejderNummer, string fornavn, string efternavn, int telefonNummer, string privatMail, string adresse,
        string adgangskode, string afdeling, int rolle, string arbejdsMail, int postNr)
        {
            SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());

            try
            {
                string query = "UPDATE SP_Ansat " +
                    "SET " +
                    "arbejdsMail = @arbejdsEmail, " +
                    "forNavn = @fornavn, " +
                    "efterNavn = @efternavn," +
                    "telefonNummer = @telefonNummer, " +
                    "adresse = @adresse, " +
                    "postNr = @postNr, " +
                    "rolle = @rolle" +
                    "password = @password, " +
                    "privatMail = @privatMail" +
                    "WHERE AnsatID = @medarbejderNummer";

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@medarbejderNummer", medarbejderNummer);
                command.Parameters.AddWithValue("@afdeling", afdeling);
                command.Parameters.AddWithValue("@arbejdsMail", arbejdsMail);
                command.Parameters.AddWithValue("@fornavn", fornavn);
                command.Parameters.AddWithValue("@efternavn", efternavn);
                command.Parameters.AddWithValue("@telefonNummer", telefonNummer);
                command.Parameters.AddWithValue("@adresse", adresse);
                command.Parameters.AddWithValue("@postNr", postNr);
                command.Parameters.AddWithValue("@rolle", rolle);
                command.Parameters.AddWithValue("@password", adgangskode);
                command.Parameters.AddWithValue("@privatMail", privatMail);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved opdatering af Ansat" + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            { 
                conn.Close(); 
            }
        }

        public void DeleteAnsat(int medarbejderNummer)
        {
            SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());
            try
            {
                string query = "DELETE FROM SP_Ansat WHERE AnsatID = @medarbejdernummer";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@medarbejderNummer", medarbejderNummer);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved sletning af Ansat" + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }

        }


        /// <summary>
        /// Martin: Retunerer en instans af Ansat med den logged ind persons data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.Ansat ReadAnsat(string id)
        {
            Models.Ansat Employee = null;
            SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());

            try
            {
                string query = "SELECT TOP 1 * FROM SP_Ansat WHERE AnsatID = @id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int medarbejderNummer = reader.GetInt32(0);
                    string afdeling = reader.GetInt32(1).ToString();
                    string arbejdsEmail = reader.GetString(2);
                    string forNavn = reader.GetString(3);
                    string efterNavn = reader.GetString(4);
                    int tlf = reader.GetInt32(5);
                    string adresse = reader.GetString(6);
                    int postNummer = reader.GetInt32(7);
                    int rolleID = reader.GetInt32(8);
                    string password = reader.GetString(9);
                    string privatMail = reader.GetString(10);

                    Employee = new Models.Ansat(forNavn, efterNavn, tlf, privatMail, adresse, medarbejderNummer, password, afdeling, rolleID, arbejdsEmail, postNummer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved læsning af Ansat" + ex.Message, "FEJL",MessageBoxButtons.OK);
            }
            finally 
            {
                conn.Close(); 
            }
            return Employee;
        }
        //public string GetNameAndRights(int AnsatID, out int rolleID)
        //{
        //    SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());
        //    string sSQL = $"SELECT fornavn, efternavn, rolleID FROM SP_Ansat WHERE AnsatID = {AnsatID}";
        //    SqlCommand command = new SqlCommand(sSQL, conn);
        //    SqlDataReader reader = command.ExecuteReader();

        //    string fullName = string.Empty;
        //    rolleID = 0;
        //    while (reader.Read())
        //    {
        //        fullName = reader.GetString(0) + " " + reader.GetString(1);
        //        rolleID = reader.GetInt32(2);
        //    }

        //    return fullName;
        //}
    }
}
