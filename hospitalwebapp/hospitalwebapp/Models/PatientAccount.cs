using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hospitalwebapp.Models
{
    public class PatientAccount
    {
        [Key]
        public int PatientId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "FirstName is required.")]
        public String FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "LastName is required.")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [DisplayName("Gender")]
        public PersonsGender PatientGender { get; set; }

        [Required(ErrorMessage = "Profession is required.")]
        public String Profession { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public String  Address { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [DisplayName("Date of birth")]
        public String DateOfBirth { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public String Email { get; set; }

       
        [DataType(DataType.PhoneNumber)]
        [StringLength(11)]
        [DisplayName("Phone Number")]
        public String Phone { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required.")]
        public String Username { get; set; }

        //Card Type is required for patient registration : Single, Family, Or Couple

        [DisplayName("Registration Type")]  
        [Required(ErrorMessage = "Registration Type is required.")]
        public RegistrationType PatientRegistrationType { get; set; }


        [Required(ErrorMessage = "Password  is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please confirm your Password.")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
      
       
    }

    public enum RegistrationType
    {
        Single,
        Couple,
        Family
    }
}