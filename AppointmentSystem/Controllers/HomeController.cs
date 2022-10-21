using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using AppointmentSystem.Models;
using System.Web.Security;

namespace AppointmentSystem.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                string conString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand("spValidateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UserId", login.UserId);
                cmd.Parameters.AddWithValue("@Password", login.Password);
                cmd.Parameters.Add("@Result", SqlDbType.Int, 1).Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
                if (!(string.IsNullOrEmpty(cmd.Parameters["@Result"].Value.ToString())))
                {
                    FormsAuthentication.SetAuthCookie(login.UserId, false);
                    TempData["PatientId"]=(int)cmd.Parameters["@Result"].Value;
                    return RedirectToAction("NewAppointment", "Patient");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect UserId/Password");
                    return View();
                }
            }
            else
                return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(PatientDetail patient)
        {
            if (ModelState.IsValid)
            {
                string conString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand("spAddPatient", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", patient.FirstName);
                cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", patient.PhoneNumber);
                cmd.Parameters.AddWithValue("@DOB", patient.DOB);
                cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                cmd.Parameters.AddWithValue("@EmailId", patient.EmailId);
                cmd.Parameters.AddWithValue("@Pwd", patient.Pwd);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                ViewData["Message"] = "New Patient added!";

                return View();
            }
            else
                return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData.Remove("PatientId");
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}
