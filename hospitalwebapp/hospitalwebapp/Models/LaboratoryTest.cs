using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hospitalwebapp.Models
{
    public class LaboratoryTest
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
        public double Price { get; set; }

    }
}