using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.Models;

namespace DAL.Employee
{
    public class EmployeeDAL
    {
        string connString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
        public List<DOL.Models.Employee> GetAllEmployees()
        {
            try
            {
                SqlConnection con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("Employee_GetAllEmployee", con);


                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataReader rdr = cmd.ExecuteReader();
                List<DOL.Models.Employee> objListEmployee = new List<DOL.Models.Employee>();
                DOL.Models.Employee objEmployee = new DOL.Models.Employee();
                while (rdr.Read())
                {
                    objEmployee = new DOL.Models.Employee();
                    
                    objEmployee.id = (int)rdr.GetValue(0);
                    objEmployee.firstName = (string)rdr.GetString(1);
                    objEmployee.email = (string)rdr.GetString(2);
                    objEmployee.salary = (double)rdr.GetValue(3);
                    objEmployee.bod = (string)rdr.GetValue(4);

                    objListEmployee.Add(objEmployee);
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return objListEmployee;
            }
            catch (Exception ex)
            {
            }
            return new List<DOL.Models.Employee>();
        }


        public DOL.Models.Employee GetEmployees(ref string strError, int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("Employee_LoadEmployee", con);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataReader rdr = cmd.ExecuteReader();
                DOL.Models.Employee objEmployee = new DOL.Models.Employee();
                while (rdr.Read())
                {
                    objEmployee = new DOL.Models.Employee();

                    objEmployee.id = (int)rdr.GetValue(0);
                    objEmployee.firstName = (string)rdr.GetString(1);
                    objEmployee.email = (string)rdr.GetString(2);
                    objEmployee.salary = (double)rdr.GetValue(3);
                    objEmployee.bod = (string)rdr.GetValue(4);
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return objEmployee;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return new DOL.Models.Employee();
        }


        public void EditEmployees(ref string strError, DOL.Models.Employee model)
        {
            try
            {
                SqlConnection con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("Employee_editEmployee", con);
                cmd.Parameters.AddWithValue("@id", model.id);
                cmd.Parameters.AddWithValue("@FirstName", model.firstName);
                cmd.Parameters.AddWithValue("@Email", model.email);
                cmd.Parameters.AddWithValue("@salary", model.salary);
                cmd.Parameters.AddWithValue("@bod", model.bod);

                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.ExecuteReader();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
        }


        public void AddEmployees(ref string strError, DOL.Models.Employee model)
        {
            try
            {
                SqlConnection con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("Employee_AddEmployee", con);
                cmd.Parameters.AddWithValue("@FirstName", model.firstName);
                cmd.Parameters.AddWithValue("@Email", model.email);
                cmd.Parameters.AddWithValue("@salary", model.salary);
                cmd.Parameters.AddWithValue("@bod", model.bod);

                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.ExecuteReader();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
        }


        public void DeleteEmployee(ref string strError, int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("Employee_DeleteEmployee", con);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.ExecuteReader();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
        }
        //
    }
}
