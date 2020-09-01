using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hospitalwebapp.Models
{
    public class PatientTest
    {
        [Key]
        public int LabTestId { get; set; }

        [Required(ErrorMessage = "Test Name is Required.")]
        [StringLength(150, MinimumLength = 2)]
        public String TestName { get; set; }

        [Required(ErrorMessage = "Test Code is Required.")]
        [StringLength(15, MinimumLength = 2)]
        public String TestCode { get; set; }


        [Required(ErrorMessage = "Short description of Medical Test is Required.")]
        [DisplayName("Medical Test Description")]
        public String Description { get; set; }

        [Required(ErrorMessage = "Price for Test is Required.")]
        [DataType(DataType.Currency)]
        public double TestPrice { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "FirstName is required.")]
        public String FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "LastName is required.")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [DisplayName("Patient Gender")]
        public String Gender { get; set; }

        //Card Number Can be used as patient's username
        [DisplayName("Card Number")]
        [Required(ErrorMessage = "Card Number is required.")]
        public String CardNumber { get; set; }
    }
}