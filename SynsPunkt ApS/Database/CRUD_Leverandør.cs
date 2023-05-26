using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Database
{
    public class CRUD_Leverandør
    {
        string connectionString = Database.ConnectionString.GetConnectionString();

        /// <summary>
        /// Opretter en ny leverandør i databasen.
        /// </summary>
        /// <param name="navn"></param>
        /// <param name="adresse"></param>
        /// <param name="email"></param>
        /// <param name="faktureringsinfo"></param>
        /// <param name="tlfNummer"></param>
        public void CreateLeverandør(string navn, string adresse, int postNr, string email, string faktureringsinfo, int tlfNummer)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.CommandText = "INSERT INTO SP_Leverandør2 (leverandørNavn, adresse, postNr, email, faktureringsOplysninger, telefonNummer) " +
                                      "VALUES ('" + navn + "', '" + adresse + "', " + postNr + ", '" + email + "', '"
                                      + faktureringsinfo + "', " + tlfNummer + ")";


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
        /// Returnere en leverandør udfra valgt ID fra databasen.
        /// </summary>
        /// <param name="levId"></param>
        /// <param name="levId2"></param>
        /// <param name="navn"></param>
        /// <param name="adresse"></param>
        /// <param name="postNr"></param>
        /// <param name="email"></param>
        /// <param name="faktureringsinfo"></param>
        /// <param name="tlfNummer"></param>
        public Models.Leverandør ReadLeverandør(string levId, out string levId2, out string navn, out string adresse, out string postNr, out string email, 
            out string faktureringsinfo, out string tlfNummer)
        {
            levId2 = "";
            navn = "";
            adresse = "";
            postNr = "";
            email = "";
            faktureringsinfo = "";
            tlfNummer = "";

            Models.Leverandør leveran = new Models.Leverandør(0, "", "", 0, "", "", 0);
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
                    leveran = new Models.Leverandør(Convert.ToInt32(reader["cvrID"]), reader["leverandørNavn"].ToString(), reader["adresse"].ToString(),
                       Convert.ToInt32(reader["postNr"]), reader["email"].ToString(), reader["faktureringsOplysninger"].ToString(), 
                       Convert.ToInt32(reader["telefonNummer"]));

                    levId2 = leveran.CVRnummer.ToString();
                    navn = leveran.LeverandørNavn;
                    adresse = leveran.Adresse;
                    postNr = leveran.PostNummer.ToString();
                    email = leveran.Email;
                    faktureringsinfo = leveran.FaktureringsOplysninger;
                    tlfNummer = leveran.TelefonNummer.ToString();
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
            return leveran;
        }

        public void UpdateLeverandør(string levId, string navn, string adresse, int postNr, string email, string faktureringsinfo, int tlfNummer)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.CommandText = "UPDATE SP_Leverandør2 SET leverandørNavn = '" + navn + "', adresse = '" + adresse +
                    "', postNr = " + postNr + ", email = '" + email + "', faktureringsOplysninger = '" + faktureringsinfo + "', telefonNummer = " + tlfNummer + " " +
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

        public void DeleteLeverandør(string levId)
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
        /// Henter alle Leverandører fra databasen og returnere en liste af models.Leverandør
        /// </summary>
        /// <returns></returns>
        public List<Models.Leverandør> GetAllLeverandør()
        {
            List<Models.Leverandør> LeverandørList = new List<Models.Leverandør>();

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
                    Models.Leverandør leverandør = new Models.Leverandør(
                       Convert.ToInt32(reader["cvrID"]),
                       reader["leverandørNavn"].ToString(),
                       reader["adresse"].ToString(),
                       Convert.ToInt32(reader["postNr"]),
                       reader["email"].ToString(),
                       reader["faktureringsOplysninger"].ToString(),
                       Convert.ToInt32(reader["telefonNummer"]));

                    LeverandørList.Add(leverandør);
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
            return LeverandørList;
        }

        public List<Models.Leverandør> SearchLeverandørByName(string name)
        {
            List<Models.Leverandør> searchResults = new List<Models.Leverandør>();


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
                    string leverandørNavn = reader["leverandørNavn"].ToString();
                    string adresse = reader["adresse"].ToString();
                    int postNr = Convert.ToInt32(reader["postNr"]);
                    string email = reader["email"].ToString();
                    string faktureringsOplysninger = reader["faktureringsOplysninger"].ToString();
                    int telefonNummer = Convert.ToInt32(reader["telefonNummer"]);


                    Models.Leverandør leverandør = new Models.Leverandør(cvrID, leverandørNavn, adresse, postNr, email, faktureringsOplysninger, telefonNummer);

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
    





 