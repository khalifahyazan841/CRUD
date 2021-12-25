using DAL.UserLogin;
using DOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UserLogin
{
    public class UserLoginBLL
    {
        public Login GetUserForLogin(Login Model)
        {
            UserLoginDAL objUserLoginDAL = new UserLoginDAL();

            Login objLogin = new Login();
            
            objLogin = objUserLoginDAL.GetUserForLogin(Model);

            if (objLogin.ID <= 0)
            {
                return null;
            }
            
            return objLogin;
        }
    }
}
