using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppointmentSystem.Models
{
    [MetadataType(typeof(DoctorDetailMetaData))]
    public partial class DoctorDetail
    {
    }
    public class DoctorDetailMetaData
    {
        [Required]
        [Display(Name = "Doctor's Name")]
        public string FirstName { get; set; }
    }
}