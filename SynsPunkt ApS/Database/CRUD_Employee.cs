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
    public class CRUD_Employee
    {

        SqlConnection conn = new SqlConnection(Database.ConnectionString.GetConnectionString());

        /// <summary>
        /// Martin: Creates an employee in the database
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
        public void CreateEmployee(string firstName, string lastName, int telephoneNumber, string privateMail, string adress,
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
        /// Martin: Updates an employees attributes
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
        public void UpdateEmployee(int employeeID, string firstName, string lastName, int telephoneNumber, string privateMail, string adress,
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

        /// <summary>
        /// Martin: Deletes an employee from the database
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeleteEmployee(int employeeID)
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
        /// Martin: Returns an instance of Employee with data based on ID (It's used to save the loggedin persons data in mainmenu)
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Models.Employee GetEmployeeByID(string employeeID)
        {
            Models.Employee Employee = null;

            try
            {
                string query = "SELECT TOP 1 * FROM SP_Ansat WHERE AnsatID = @id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@id", employeeID);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int employee_ID = Convert.ToInt32(reader["AnsatID"]);
                    string department = reader["LokationID"].ToString();
                    string workMail = reader["arbejdsMail"].ToString();
                    string firstName = reader["forNavn"].ToString();
                    string lastName = reader["efterNavn"].ToString();
                    int phoneNumber = Convert.ToInt32(reader["telefonNummer"]);
                    string adress = reader["adresse"].ToString();
                    int zipCode = Convert.ToInt32(reader["postNr"]);
                    int roleID = Convert.ToInt32(reader["rolleID"]);
                    string password = reader["password"].ToString();
                    string privateMail = reader["privatMail"].ToString();

                    Employee = new Models.Employee(firstName, lastName, phoneNumber, privateMail, adress, employee_ID, password, department, roleID, workMail, zipCode);
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

        /// <summary>
        /// Martin: Returns a list of employee based on the search string, which is the full name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Models.Employee> SearchEmployeeByName(string name)
        {
            List<Models.Employee> searchResults = new List<Models.Employee>();

            try
            {
                string query = "SELECT * FROM SP_Ansat WHERE CONCAT (forNavn, ' ', efterNavn) LIKE '%' + @name + '%'";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@name", name);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int employeeID = Convert.ToInt32(reader["AnsatID"]);
                    string department = reader["LokationID"].ToString();
                    string workMail = reader["arbejdsMail"].ToString();
                    string firstName = reader["forNavn"].ToString();
                    string lastName = reader["efterNavn"].ToString();
                    int phoneNumber = Convert.ToInt32(reader["telefonNummer"]);
                    string adress = reader["adresse"].ToString();
                    int zipCode = Convert.ToInt32(reader["postNr"]);
                    int roleID = Convert.ToInt32(reader["rolleID"]);
                    string password = reader["password"].ToString();
                    string privateMail = reader["privatMail"].ToString();

                    Models.Employee Employee = new Models.Employee(firstName, lastName, phoneNumber, privateMail, adress, employeeID, password, department, roleID, workMail, zipCode);

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

        /// <summary>
        /// Martin: Returns all employees in the database as a list
        /// </summary>
        /// <returns></returns>
        public List<Models.Employee> GetAllEmployees()
        {
            List<Models.Employee> allEmployees = new List<Models.Employee>();

            try
            {
                string query = "SELECT * FROM SP_Ansat";
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int employeeID = Convert.ToInt32(reader["AnsatID"]);
                    string department = reader["LokationID"].ToString();
                    string workMail = reader["arbejdsMail"].ToString();
                    string firstName = reader["forNavn"].ToString();
                    string lastName = reader["efterNavn"].ToString();
                    int phoneNumber = Convert.ToInt32(reader["telefonNummer"]);
                    string adress = reader["adresse"].ToString();
                    int zipCode = Convert.ToInt32(reader["postNr"]);
                    int roleID = Convert.ToInt32(reader["rolleID"]);
                    string password = reader["password"].ToString();
                    string privateMail = reader["privatMail"].ToString();

                    Models.Employee Employee = new Models.Employee(firstName, lastName, phoneNumber, privateMail, adress, employeeID, password, department, roleID, workMail, zipCode);

                    allEmployees.Add(Employee);
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
            return allEmployees;
        }

        /// <summary>
        /// Martin: Gets the department name based on the employees locationID
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Martin: Gets the rolename based on the employees roleID
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
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
    }
}
