using school_management_app.Models;
using school_management_app.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace school_management_app.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult ShowResult()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ShowResult1(ResultModel resultModel)
        {
            string connection = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                string query = "select * from StudentResult where RollNumber = @roll and Course=@course";

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@roll", resultModel.RollNumber);
                cmd.Parameters.AddWithValue("@course", resultModel.Course);

                try
                {
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ShowResultModel showResultModel = new ShowResultModel()
                            {
                                RollNumber = Convert.ToInt32(reader["RollNumber"]),
                                Name = reader["Name"].ToString(),
                                Course = reader["Course"].ToString(),
                                Math = reader["Math"].ToString(),
                                Hindi = reader["Hindi"].ToString(),
                                English = reader["English"].ToString(),
                                Gramer = reader["Gramer"].ToString(),
                                SocialScience = reader["SocialScience"].ToString()
                            };

                            return View(showResultModel);
                        }
                        else
                        {
                            // Handle the case where no data is found for the given ID
                            return RedirectToAction("NotFound");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle and log the exception, or return an error view
                    return View("Error");
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            StudentDat student = new StudentDat();
            var result = student.Deletestudent(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Delete(StudentRagistrationModel model)
        {
            StudentDat student = new StudentDat();
            bool result = student.DeleteByiD(model.id);
            if (result)
            {
                ViewBag.Message = "data deleted";

            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            StudentDat student = new StudentDat();
            var result = student.EditGetByid(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(StudentRagistrationModel studentRagistrationModel)

        {
            StudentDat studentDat = new StudentDat();
            bool result = studentDat.UpadteRow(studentRagistrationModel);
            if (result)
            {
                ViewBag.Message = "updated data";
            }
              return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(AdminLoginModel adminLoginModel)
        {
            StudentDat studentDat = new StudentDat();
            bool result = studentDat.CheckLogin(adminLoginModel);
            if (result)
            {
                return RedirectToAction("GetAllData", "Home");
            }
            else
            {
                ViewBag.Message = "id or pasword is incorrect";
            }
            return View();
        }
        public ActionResult GetAllData()
        {
            StudentDat studentDat = new StudentDat();
            var Listalldata = studentDat.GetAllDetails();
            return View(Listalldata);
        }
        public ActionResult StudentRagistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentRagistration(StudentRagistrationModel studentRagistrationModel)
        {
            StudentDat studentDat = new StudentDat();
            bool result = studentDat.UserRagistration(studentRagistrationModel);
            if (result)
            {
                ViewBag.Message = "data inserted";

            }
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}