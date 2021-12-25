using BLL.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["LogedUser"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult Logout() {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult AddEditEmployee(int id)
        {
            if (id > 0)
            {
                ViewBag.Type = "Edit User";
                ViewBag.state = "Edit";
                DOL.Models.Employee employees = new DOL.Models.Employee();
                employees = getUserDataForEdit(id);
                return View(employees);
            }
            else
            {
                ViewBag.state = "Add";
                ViewBag.Type = "Add User";
                return View();
            }
        }


        [HttpPost]
        public ActionResult SubmitAddEditEmployee(DOL.Models.Employee model)
        {
            if (model.id > 0)
            {
                EditEmployee(model);
                return RedirectToAction("Index" , "Home");
            }
            else
            {
                AddEmployee(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        public ActionResult DeleteEmployee(int id)
        {
            EmployeeBLL objEmployeeBLL = new EmployeeBLL();
            objEmployeeBLL.DeleteEmployee(id);
            return RedirectToAction("Index", "Home");
        }

        private DOL.Models.Employee getUserDataForEdit(int id)
        {
            EmployeeBLL objEmployeeDAL = new EmployeeBLL();
            DOL.Models.Employee employees = new DOL.Models.Employee();
            employees = objEmployeeDAL.getEmployee(id);
            return employees;
        }


        private void AddEmployee(DOL.Models.Employee model)
        {
            EmployeeBLL objEmployeeBLL = new EmployeeBLL();
            objEmployeeBLL.AddEmployee(model);
        }        



        private void EditEmployee(DOL.Models.Employee model)
        {
            EmployeeBLL objEmployeeBLL = new EmployeeBLL();
            objEmployeeBLL.EditEmployee(model);
        }





        [HttpPost]
        public async Task<JsonResult> LoadAllEmployees()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                EmployeeBLL objEmployeeDAL = new EmployeeBLL();
                List<DOL.Models.Employee> employeesList = new List<DOL.Models.Employee>();
                employeesList = objEmployeeDAL.getAllEmployees();
                if (employeesList == null)
                {
                    employeesList = new List<DOL.Models.Employee>();
                }


                //Sorting 
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    employeesList = employeesList.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }


                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    employeesList = employeesList.Where(m => (m.email.Contains(searchValue)) || (m.email.ToLower().Contains(searchValue))
                    || (m.firstName.Contains(searchValue)) || (m.firstName.ToLower().Contains(searchValue))).ToList();
                }

                //total number of rows count
                recordsTotal = employeesList.Count();
                //Paging     
                var data = employeesList.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}