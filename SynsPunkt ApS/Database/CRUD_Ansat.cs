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

        SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());

        /// <summary>
        /// Martin: Opretter en Ansat i databasen
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="privateMail"></param>
        /// <param name="adress"></param>
        /// <param name="password"></param>
        /// <param name="department"></param>
        /// <param name="roleID"></param>
        /// <param name="workMail"></param>
        /// <param name="zipCode"></param>
        public void CreateAnsat(string firstName, string lastName, int telephoneNumber, string privateMail, string adress,
        string password, string department, string roleID, string workMail, int zipCode)
        {
            try
            {
                string query = "INSERT INTO SP_Ansat " +
                    "VALUES ((SELECT LokationID FROM SP_Optiker WHERE byNavn = @afdeling), " +
                    "@arbejdsMail, @fornavn, @efternavn, @telefonNummer, @adresse, @postNr, " +
                    "(SELECT rolleID FROM SP_Rolle WHERE rolleNavn = @rolle), " +
                    "@password, @privatMail)";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@afdeling", department);
                command.Parameters.AddWithValue("@arbejdsMail", workMail);
                command.Parameters.AddWithValue("@fornavn", firstName);
                command.Parameters.AddWithValue("@efternavn", lastName);
                command.Parameters.AddWithValue("@telefonNummer", telephoneNumber);
                command.Parameters.AddWithValue("@adresse", adress);
                command.Parameters.AddWithValue("@postNr", zipCode);
                command.Parameters.AddWithValue("@rolle", roleID);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@privatMail", privateMail);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved oprettelse af Ansat. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Martin: Opdater en Ansats attributter
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="privateMail"></param>
        /// <param name="adress"></param>
        /// <param name="password"></param>
        /// <param name="department"></param>
        /// <param name="roleID"></param>
        /// <param name="workMail"></param>
        /// <param name="zipCode"></param>
        public void UpdateAnsat(int employeeID, string firstName, string lastName, int telephoneNumber, string privateMail, string adress,
        string password, string department, string roleID, string workMail, int zipCode)
        {

            try
            {
                string query = "UPDATE SP_Ansat " +
                    "SET " +
                    "LokationID = (SELECT LokationID From SP_Optiker WHERE byNavn = @departmentID), " +
                    "arbejdsMail = @arbejdsMail, " +
                    "forNavn = @fornavn, " +
                    "efterNavn = @efternavn," +
                    "telefonNummer = @telefonNummer, " +
                    "adresse = @adresse, " +
                    "postNr = @postNr, " +
                    "rolleID = (SELECT rolleID From SP_Rolle WHERE rolleNavn = @rolle), " +
                    "password = @password, " +
                    "privatMail = @privatMail " +
                    "WHERE AnsatID = @medarbejderNummer";

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@departmentID", department);
                command.Parameters.AddWithValue("@arbejdsMail", workMail);
                command.Parameters.AddWithValue("@fornavn", firstName);
                command.Parameters.AddWithValue("@efternavn", lastName);
                command.Parameters.AddWithValue("@telefonNummer", telephoneNumber);
                command.Parameters.AddWithValue("@adresse", adress);
                command.Parameters.AddWithValue("@postNr", zipCode);
                command.Parameters.AddWithValue("@rolle", roleID);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@privatMail", privateMail);
                command.Parameters.AddWithValue("@medarbejderNummer", employeeID);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved opdatering af Ansat. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteAnsat(int employeeID)
        {
            try
            {
                string query = "DELETE FROM SP_Ansat WHERE AnsatID = @medarbejdernummer";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@medarbejderNummer", employeeID);

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
        /// Martin: Retunerer en instans af Ansat med data baseret på ID (Bruges til at gemme den logged-inds persons data i mainmenu)
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Models.Ansat GetAnsatByID(string employeeID)
        {
            Models.Ansat Employee = null;

            try
            {
                string query = "SELECT TOP 1 * FROM SP_Ansat WHERE AnsatID = @id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@id", employeeID);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int medarbejderNummer = Convert.ToInt32(reader["AnsatID"]);
                    string afdeling = reader["LokationID"].ToString();
                    string arbejdsEmail = reader["arbejdsMail"].ToString();
                    string forNavn = reader["forNavn"].ToString();
                    string efterNavn = reader["efterNavn"].ToString();
                    int tlf = Convert.ToInt32(reader["telefonNummer"]);
                    string adresse = reader["adresse"].ToString();
                    int postNummer = Convert.ToInt32(reader["postNr"]);
                    int rolleID = Convert.ToInt32(reader["rolleID"]);
                    string password = reader["password"].ToString();
                    string privatMail = reader["privatMail"].ToString();

                    Employee = new Models.Ansat(forNavn, efterNavn, tlf, privatMail, adresse, medarbejderNummer, password, afdeling, rolleID, arbejdsEmail, postNummer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved læsning af Ansat. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
            return Employee;
        }

        public List<Models.Ansat> SearchAnsatByName(string name)
        {
            List<Models.Ansat> searchResults = new List<Models.Ansat>();

            try
            {
                string query = "SELECT * FROM SP_Ansat WHERE CONCAT (forNavn, ' ', efterNavn) LIKE '%' + @name + '%'";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@name", name);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int medarbejderNummer = Convert.ToInt32(reader["AnsatID"]);
                    string afdeling = reader["LokationID"].ToString();
                    string arbejdsEmail = reader["arbejdsMail"].ToString();
                    string forNavn = reader["forNavn"].ToString();
                    string efterNavn = reader["efterNavn"].ToString();
                    int tlf = Convert.ToInt32(reader["telefonNummer"]);
                    string adresse = reader["adresse"].ToString();
                    int postNummer = Convert.ToInt32(reader["postNr"]);
                    int rolleID = Convert.ToInt32(reader["rolleID"]);
                    string password = reader["password"].ToString();
                    string privatMail = reader["privatMail"].ToString();

                    Models.Ansat Employee = new Models.Ansat(forNavn, efterNavn, tlf, privatMail, adresse, medarbejderNummer, password, afdeling, rolleID, arbejdsEmail, postNummer);

                    searchResults.Add(Employee);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved søgning af Ansat. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }

            return searchResults;
        }


        public List<Models.Ansat> GetAllAnsat()
        {
            List<Models.Ansat> allAnsat = new List<Models.Ansat>();

            try
            {
                string query = "SELECT * FROM SP_Ansat";
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int medarbejderNummer = Convert.ToInt32(reader["AnsatID"]);
                    string afdeling = reader["LokationID"].ToString();
                    string arbejdsEmail = reader["arbejdsMail"].ToString();
                    string forNavn = reader["forNavn"].ToString();
                    string efterNavn = reader["efterNavn"].ToString();
                    int tlf = Convert.ToInt32(reader["telefonNummer"]);
                    string adresse = reader["adresse"].ToString();
                    int postNummer = Convert.ToInt32(reader["postNr"]);
                    int rolleID = Convert.ToInt32(reader["rolleID"]);
                    string password = reader["password"].ToString();
                    string privatMail = reader["privatMail"].ToString();

                    Models.Ansat Employee = new Models.Ansat(forNavn, efterNavn, tlf, privatMail, adresse, medarbejderNummer, password, afdeling, rolleID, arbejdsEmail, postNummer);

                    allAnsat.Add(Employee);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved at få alle ansatte. " + ex.Message, "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
            return allAnsat;
        }

        public string GetDepartmentName(int departmentID)
        {
            string departmentName = null;
            try
            {
                string query = "SELECT byNavn From SP_Optiker WHERE LokationID = @departmentID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@departmentID", departmentID);

                conn.Open();
                departmentName = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved hentning af afdelingsnavn. ", "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
            return departmentName;
        }
        public string GetRoleName(int roleID)
        {
            string roleName = null;
            try
            {
                string query = "SELECT rolleNavn From SP_Rolle WHERE rolleID = @roleID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@roleID", roleID);

                conn.Open();
                roleName = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved hentning af rollenavn. ", "FEJL", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
            return roleName;
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


        //int medarbejderNummer = reader.GetInt32(0);
        //string afdeling = reader.GetInt32(1).ToString();
        //string arbejdsEmail = reader.GetString(2);
        //string forNavn = reader.GetString(3);
        //string efterNavn = reader.GetString(4);
        //int tlf = reader.GetInt32(5);
        //string adresse = reader.GetString(6);
        //int postNummer = reader.GetInt32(7);
        //int rolleID = reader.GetInt32(8);
        //string password = reader.GetString(9);
        //string privatMail = reader.GetString(10);

    }
}
