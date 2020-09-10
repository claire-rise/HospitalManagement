using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace hospitalwebapp.Models
{
    public class EmailModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Full name")]
        [Required(ErrorMessage = "Fullname is required!")]
        public String Fullname { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Invalid Email!")]
        public String UserEmail { get; set; }

        [Required(ErrorMessage = "Message is required!")]
        [AllowHtml]
        public String Message { get; set; }
    }
}