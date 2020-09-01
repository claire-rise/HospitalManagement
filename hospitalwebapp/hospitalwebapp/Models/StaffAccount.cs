using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hospitalwebapp.Models
{
    public class StaffAccount
    {
        [Key]
        public int StaffId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "FirstName is required.")]
        public String FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "LastName is required.")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [DisplayName("Employee Gender")]
        public PersonsGender StaffGender { get; set; }

        [Required(ErrorMessage = "Profession is required.")]
        public String Profession { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [DisplayName("Department")]
        public StaffDepartments StaffDepartment { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [DisplayName("Date of birth")]
        public String DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public String Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11)]
        [DisplayName("Phone Number")]
        public String Phone { get; set; }

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

    public enum PersonsGender
    {  
        Male,
        Female
    }


    //Public class attribute for Various Departments in the Hospital
    public enum StaffDepartments
    {
        Admin,
        Doctor,
        Nurse,
        Laboratory,
        Pharmacy,
        Accounting,
        Reception
    }



}