using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    public class Employee_service
    {
        private Database.CRUD_Employee crudEmployee = new Database.CRUD_Employee();

        /// <summary>
        /// Martin: Takes input and calls database method to create an employee in the database
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="privateMail"></param>
        /// <param name="adress"></param>
        /// <param name="password"></param>
        /// <param name="department"></param>
        /// <param name="role"></param>
        /// <param name="workMail"></param>
        /// <param name="zipCode"></param>
        public void CreateEmployee(string firstName, string lastName, int phoneNumber, string privateMail, string adress,
        string password, string department, string role, string workMail, int zipCode)
        {
            crudEmployee.CreateEmployee(firstName, lastName, phoneNumber, privateMail, adress, password, department, role, workMail, zipCode);

        }

        /// <summary>
        /// Martin: Takes input and calls database method to update an employee in the database
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="privateMail"></param>
        /// <param name="adress"></param>
        /// <param name="password"></param>
        /// <param name="department"></param>
        /// <param name="role"></param>
        /// <param name="workMail"></param>
        /// <param name="zipCode"></param>
        public void UpdateEmployee(int employeeID, string firstName, string lastName, int phoneNumber, string privateMail, string adress,
        string password, string department, string role, string workMail, int zipCode)
        {
            crudEmployee.UpdateEmployee(employeeID, firstName, lastName, phoneNumber, privateMail, adress,
                                  password, department, role, workMail, zipCode);
        }

        /// <summary>
        /// Martin: Takes a single input and calls database method to delete an employee from the database
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeleteEmployee(int employeeID)
        {
            crudEmployee.DeleteEmployee(employeeID);
        }

        /// <summary>
        /// Martin: Takes a single input, uses database method to get the correct employee and returns said employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.Employee GetEmployeeByID(string id)
        {
            Models.Employee employee = crudEmployee.GetEmployeeByID(id);
            return employee;
        }

        /// <summary>
        /// Martin: Takes a search string input, uses database method to get a list of employees and returns said list
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Models.Employee> SearchEmployeeByName(string name)
        {
            List<Models.Employee> searchResults = crudEmployee.SearchEmployeeByName(name);

            return searchResults;
        }

        /// <summary>
        /// Martin: Uses database method to get a list of all employees and then returns said list
        /// </summary>
        /// <returns></returns>
        public List<Models.Employee> GetAllEmployees()
        {
            List<Models.Employee> allEmployees = crudEmployee.GetAllEmployees();

            return allEmployees;
        }

        /// <summary>
        /// Martin: Takes a single input, gets the name of the department based on the input and returns that name
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public string GetDepartmentName(int departmentID)
        {
            string departmentName = crudEmployee.GetDepartmentName(departmentID);

            return departmentName;
        }

        /// <summary>
        /// Martin: Takes a single input, gets the role name based on input and returns that name
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public string GetRoleName(int roleID)
        {
            string roleName = crudEmployee.GetRoleName(roleID);

            return roleName;
        }
    }
}
