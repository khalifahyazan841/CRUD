using DAL.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Employee
{
    public class EmployeeBLL
    {
        public List<DOL.Models.Employee> getAllEmployees() {
            EmployeeDAL objEmployeeDAL = new EmployeeDAL();
            List<DOL.Models.Employee> employeesList = new List<DOL.Models.Employee>();

            employeesList = objEmployeeDAL.GetAllEmployees();
            if (employeesList.Count() <= 0)
            {
                return null;
            }
            return employeesList;
        }


        public DOL.Models.Employee getEmployee(int id)
        {
            string strError = "";
            EmployeeDAL objEmployeeDAL = new EmployeeDAL();
            DOL.Models.Employee employees = new DOL.Models.Employee();

            employees = objEmployeeDAL.GetEmployees(ref strError , id);
            if (strError != "")
            {
                return null;
            }
            if (employees != null)
            {
                return employees;
            }
            return null;
        }


        public bool EditEmployee(DOL.Models.Employee model)
        {
            string strError = "";
            EmployeeDAL objEmployeeDAL = new EmployeeDAL();
 
            objEmployeeDAL.EditEmployees(ref strError, model);
            if (strError != "")
            {
                return false;
            }
            return true;
        }


        public bool AddEmployee(DOL.Models.Employee model)
        {
            string strError = "";
            EmployeeDAL objEmployeeDAL = new EmployeeDAL();

            objEmployeeDAL.AddEmployees(ref strError, model);
            if (strError != "")
            {
                return false;
            }
            return true;
        }


        public bool DeleteEmployee(int id)
        {
            string strError = "";
            EmployeeDAL objEmployeeDAL = new EmployeeDAL();

            objEmployeeDAL.DeleteEmployee(ref strError, id);
            if (strError != "")
            {
                return false;
            }
            return true;
        }
    }
}
