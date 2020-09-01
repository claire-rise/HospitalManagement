using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hospitalwebapp.Models
{
    public class NurseVitalRecords
    {
        [Key]
        public int RecordId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "FirstName is required.")]
        public String PatientFirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public String PatientLastName { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required.")]
        public String PatientUserName { get; set; }

        [DisplayName("Body Temperature")]
        [Required(ErrorMessage = "Body Temperature is required.")]
        public String Temperature { get; set; }

        [DisplayName("Blood Pressure")]
        [Required(ErrorMessage = "Blood Pressure is required.")]
        public String BloodPressure { get; set; }

        [DisplayName("Patient Weight")]
        [Required(ErrorMessage = "Patient Weight is required.")]
        public String Weight { get; set; }

        [DisplayName("Patient Height")]
        [Required(ErrorMessage = "Patient Height is required.")]
        public String Height { get; set; }

        [DisplayName("Patient Pulse Rate")]
        [Required(ErrorMessage = "Patient Pulse Rate is required.")]
        public String PulseRate { get; set; }

        [DisplayName("Patient Respiration")]
        [Required(ErrorMessage = "Patient Respiration is required.")]
        public String Respiration { get; set; }

        [DisplayName("Body Mass Index")]
        [Required(ErrorMessage = "Patient Body Mass Index is required.")]
        public String BMI { get; set; }
    }
}