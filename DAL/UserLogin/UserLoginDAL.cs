using System.Data.SqlClient;
using System.Configuration;
using DOL.Models;
using System.Data;
using System;

namespace DAL.UserLogin
{

    public class UserLoginDAL
    {
        string connString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
        public Login GetUserForLogin(Login Model)
        {
            try
            {
                SqlConnection con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("User_GetUserForLogin", con);


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", Model.Username);
                cmd.Parameters.AddWithValue("@Password", Model.Password);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataReader rdr = cmd.ExecuteReader();
                Login objLogin = new Login();
                while (rdr.Read())
                {
                    objLogin = new Login();
                    objLogin.ID = (int)rdr.GetValue(0);
                    objLogin.Username = (string)rdr.GetString(1);
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return objLogin;
            }
            catch (Exception ex)
            {
            }
            return new Login();
        }
    }
}
