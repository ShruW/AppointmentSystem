using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppointmentSystem.Models
{
    [MetadataType(typeof(PatientDetailMetaData))]
    public partial class PatientDetail
    {
        public string ConfirmPwd { get; set; }
    }

    public class PatientDetailMetaData
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString ="{0:d}")]
        [Range(typeof(DateTime),"01/01/1970","01/01/2000",ErrorMessage = "Date of birth must be between \"01/01/1970\" and \"01/01/2000\"")]
        public DateTime? DOB { get; set; }

        [Required]
        public string Gender { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Pwd { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Pwd")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPwd { get; set; }
        
    }
}