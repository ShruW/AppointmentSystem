using AppointmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Remoting.Messaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace AppointmentSystem.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        [HttpGet]
        public ActionResult NewAppointment()
        {
            AppointmentDBContext appointmentDBContext = new AppointmentDBContext();
            List<SelectListItem> doctorsList = new List<SelectListItem>();

            foreach (DoctorDetail doctor in appointmentDBContext.DoctorDetails)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = doctor.FirstName + " " + doctor.LastName,
                    Value = doctor.DId.ToString()
                };
                doctorsList.Add(selectListItem);
            }

            DoctorsViewModel doctorsViewModel = new DoctorsViewModel();
            doctorsViewModel.DoctorsList = doctorsList;
            return View(doctorsViewModel);

            // return View();

            //var doctors = appointmentDBContext.DoctorDetails.ToList();
            //var viewModel=new DoctorsViewModel { DoctorsList = doctors };
            // return View(viewModel);
        }

        [HttpPost]
        public string  NewAppointment(DoctorsViewModel doctorsViewModel)//FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                string conString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand("spNewAppointment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PatientId", TempData["PatientId"]);
                cmd.Parameters.AddWithValue("@DoctorId", doctorsViewModel.SelectedDoctor);//formCollection["SelectedDoctor"]);
                cmd.Parameters.AddWithValue("@AppointmentDate", doctorsViewModel.AppointmentDate); //formCollection["AppointmentDate"]);
                cmd.Parameters.AddWithValue("@StartTime", doctorsViewModel.StartTime); //formCollection["StartTime"]); ;
                cmd.Parameters.AddWithValue("@Details", doctorsViewModel.Details); //formCollection["Details"]);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                //ViewBag.Message = "Your appointment is booked!";
                return "Your appointment is booked!";
                //return View(doctorsViewModel);
            }
            else
                //ViewBag.Message= "Your appointment is not booked. Please re-enter the details!";
            return "Your appointment is not booked. Please re-enter the details!"; ;
        }
        // GET: Patient/Details/5
        public ActionResult Details()
        {
            //string conString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            //SqlConnection con = new SqlConnection(conString);
            //SqlCommand cmd = new SqlCommand("select * from Appointments where PatientId=@PatientId", con);
            //cmd.CommandType = CommandType.Text;

            //cmd.Parameters.AddWithValue("@PatientId", TempData["PatientId"]);

            //con.Open();
            //cmd.ExecuteReader();
            //con.Close();

            AppointmentDBContext dbcontext = new AppointmentDBContext();
            List<Appointment> appointments = dbcontext.Appointments.ToList();
            List<Appointment> appointmentList= appointments.Where(x => x.PatientId == (int)TempData["PatientId"]).ToList();
            //App employee = dbcontext.Employees.Single(emp => emp.EmployeeId == id);
            return View(appointmentList);
        }

        //public ActionResult Reschedule(Appointment appointment)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }
        //}

        //// GET: Patient/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Patient/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Patient/Edit/5
        [HttpGet]
        public ActionResult Edit()
        {
            int id = (int)TempData["PatientId"];
            AppointmentDBContext dBContext=new AppointmentDBContext();
          PatientDetail patientDetail=  dBContext.PatientDetails.Single(x => x.PId ==id);
        return View(patientDetail);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(PatientDetail patientDetail)
        {
            if (ModelState.IsValid)
            {
                string conString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand("spEditDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PId", patientDetail.PId);
                cmd.Parameters.AddWithValue("@FirstName", patientDetail.FirstName); 
                cmd.Parameters.AddWithValue("@LastName", patientDetail.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", patientDetail.PhoneNumber); 
                cmd.Parameters.AddWithValue("@DOB", patientDetail.DOB); 
                cmd.Parameters.AddWithValue("@Gender", patientDetail.Gender);
                cmd.Parameters.AddWithValue("@EmailId", patientDetail.EmailId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                ViewBag.Message = "Details updated successfully";
                return View();
            }
            else
            {
                ViewBag.Message = "Details not updated";
                return View();
            }
        }

        public ActionResult FindDoctor()
        {
            AppointmentDBContext appointmentDBContext = new AppointmentDBContext();
            List<DoctorDetail> doctors = appointmentDBContext.DoctorDetails.ToList();

            return View(doctors);
        }

        public ActionResult DoctorDetails(int id)
        {
            AppointmentDBContext appointmentDBContext = new AppointmentDBContext();
            DoctorDetail doctor = appointmentDBContext.DoctorDetails.Single(x => x.DId == id);

            return View(doctor);
        }

        //// GET: Patient/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Patient/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
