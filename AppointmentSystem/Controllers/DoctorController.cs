using AppointmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace AppointmentSystem.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        //public ActionResult MyAppointments()
        //{
        //    AppointmentDBContext appointmentDBContext = new AppointmentDBContext();
        //    int id = (int)TempData["DoctorId"];
        //    List<Appointment> appointments = appointmentDBContext.Appointments.Where(x => x.DoctorId == id);
        //    return View();
        //}

        // GET: Doctor/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Doctor/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Doctor/Create
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

        //// GET: Doctor/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Doctor/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Doctor/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Doctor/Delete/5
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
