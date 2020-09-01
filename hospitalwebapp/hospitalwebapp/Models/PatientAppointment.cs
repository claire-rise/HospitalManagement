using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hospitalwebapp.Models
{
    public class PatientAppointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "FirstName is required.")]
        public String PatientFirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public String PatientLastName { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required.")]
        public String PatientUserName { get; set; }

        [DisplayName("Name of Clinician(Doctor)")]
        [Required(ErrorMessage = "Clinician Name is required.")]
        public List<Doctor>Doctors { get; set; }

        [DisplayName("Specify Date of Appointment")]
        [Required(ErrorMessage = "Date of Appointment is required.")]
        public DateTime AppointmentDate { get; set; }
       
    }
}