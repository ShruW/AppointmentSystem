using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppointmentSystem.Models
{
    public class DoctorsViewModel
    {
        public int DoctorId { get; set; }
        public IEnumerable<SelectListItem> DoctorsList { get; set; }

        [Required]
        [Display(Name ="Doctor")]
        public IEnumerable<string> SelectedDoctor { get; set; }
        //public int SelectedDoctor { get; set; }


        [Required]
        [Display(Name = "Appointment Date")]
        public Nullable<System.DateTime> AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public string Details { get; set; }
    }
}