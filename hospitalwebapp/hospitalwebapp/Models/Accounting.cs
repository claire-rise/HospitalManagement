using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hospitalwebapp.Models
{
    public class Accounting
    {
        [Key]
        public int AccountingId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "FirstName is required.")]
        public String FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "LastName is required.")]
        public String LastName { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required.")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Password  is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Compare("Password", ErrorMessage = "Please confirm your Password.")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}