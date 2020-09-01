using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


//Add
using hospitalwebapp.Models;

namespace hospitalwebapp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Staff Account
        public ActionResult Index()
        {
            // Show a list of all registered Staffs
            using (HospitalDbContext db = new HospitalDbContext())
            {
                var user = db.StaffAccounts.ToList();

                return View(user);
            }

        }

        // GET: Patient Account

        public ActionResult HospitalPatients()
        {
            // Show a list of all registered Patients
            using (HospitalDbContext db = new HospitalDbContext())
            {
                var user = db.PatientAccounts.ToList();

                return View(user);
            }
        }

        //Get
        public ActionResult Registration()
        {

            return View();
        }


        //Staff Registration Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(StaffAccount account)
        {
            var SelectedDepartment = "";
            if (ModelState.IsValid)
            {
                using (HospitalDbContext db = new HospitalDbContext())
                {
                    //Capture the selected Department from the user's input 
                    SelectedDepartment = account.StaffDepartment.ToString();

                     //Check if Username and Email already exist in the database. This allows to prevent
                     //Double registration 

                     StaffAccount searchObjectUsername = db.StaffAccounts.FirstOrDefault(c => c.Username == account.Username.ToString());

                    StaffAccount searchObjectEmail = db.StaffAccounts.FirstOrDefault(c => c.Email == account.Email.ToString());




                    //If Username or Email already Exists, throw a message
                    if (searchObjectUsername == null && searchObjectEmail == null)
                    {
                      
                            db.StaffAccounts.Add(account);
                            db.SaveChanges();

                        // Clear Controls 
                        ModelState.Clear();
                        // Display notification to the user
                        ViewBag.Message = ($"{account.LastName} {account.FirstName}  Successfully Registered as {SelectedDepartment}!");
                        
                    }
                    else
                    {
                        ViewBag.Error = $"Either Username or Email already exist in the database!";
                    }                  
                }               
            }
            return View();
        }


        //Get
        public ActionResult PatientRegistration()
        {

            return View();
        }

        //Method Overload for Registration Action Method ..... Patients Registration

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientRegistration(PatientAccount account)
        {
            if (ModelState.IsValid)
            {
                using (HospitalDbContext db = new HospitalDbContext())
                {
                    db.PatientAccounts.Add(account);
                    db.SaveChanges();
                }

                // Clear Controls 
                ModelState.Clear();
                // Display notification to the user
                ViewBag.Message = $"{account.LastName} {account.FirstName} Successfully Registered as Patient.";
            }
            return View();
        }


        public ActionResult AllPatients()
        {
            using (HospitalDbContext db = new HospitalDbContext())
            {
                var user = db.PatientAccounts.ToList();

                return View(User);
            }
        }

        //Get
        public ActionResult Login()
        {
            return View();
        }

  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(StaffAccount user)
        {

            try
            {
                using (HospitalDbContext db = new HospitalDbContext())
                {
                    var usr = db.StaffAccounts.Single(u => u.Username == user.Username && u.Password == user.Password);

                    if (usr != null)
                    {
                        Session["StaffId"] = usr.StaffId.ToString();
                        Session["Username"] = usr.Username.ToString();
                        /*Session["StaffDepartment"] = usr.StaffDepartment.ToString();*/

                        // Redirect user to their Respective Dashboards
                        //if user's department is Admin Redirect to the Admin Dashboard
                        /*if (usr.StaffDepartment.ToString().Equals("Admin"))
                        {
                            return RedirectToAction("Index", "Admins");
                        }
                        //if user's department is Doctor Redirect to the Doctor Dashboard
                        else if (usr.StaffDepartment.ToString().Equals("Doctor"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        //if user's department is Nurse Redirect to the Nurse Dashboard
                        else if (usr.StaffDepartment.ToString().Equals("Nurse"))
                        {
                            return RedirectToAction("Index", "Nurse");
                        }
                        //if user's department is Doctor Redirect to the Doctor Dashboard
                        else if (usr.StaffDepartment.ToString().Equals("Laboratory"))
                        {
                            return RedirectToAction("Index", "Laboratory");
                        }
                        //if user's department is Pharmacy Redirect to the Pharmacy Dashboard
                        else if (usr.StaffDepartment.ToString().Equals("Pharmacy"))
                        {
                            return RedirectToAction("Index", "Pharmacy");
                        }
                        //if user's department is Accounting Redirect to the Accounting Dashboard
                        else if (usr.StaffDepartment.ToString().Equals("Accounting"))
                        {
                            return RedirectToAction("Index", "Accounting");
                        }
                        //if user's department is Reception Redirect to the Reception Dashboard
                        else if (usr.StaffDepartment.ToString().Equals("Reception"))
                        {
                            return RedirectToAction("Index", "Reception");
                        }*/
                        return RedirectToAction("Index", "Admins");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or Password is Invalid!");
                    }
                }
                return View();
            }
            catch (Exception)
            {
                ViewBag.Error = $"Either username or Password is Invalid";

            }
            return View();
            
        }



        //Login Session for Administrators
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Login(Admin user)
         {

             using (HospitalDbContext db = new HospitalDbContext())
             {
                 var usr = db.Admins.Single(u => u.Username == user.Username && u.Password == user.Password);

                 if (usr != null)
                 {
                     Session["AdminId"] = usr.AdminId.ToString();
                     Session["Username"] = usr.Username.ToString();

                     // Redirect user to the Admin Dashboard
                     return RedirectToAction("Index", "Admin");

                 }
                 else
                 {
                     ModelState.AddModelError("", "Username or Password is Invalid!");
                 }
             }
             return View();
         }


         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Login(string username, string password)
         {
             //Setting a Static Authorized Login for Admin
             string DbUser = "admin";
             string DbPassword = "154321";

             if (DbUser.Equals(username) && DbPassword.Equals(password))
             {
                 Session["Admin"] = new Admin() { AdminId = 1, FirstName = "Olusola", LastName = "Ofoetan", Username = DbUser, Password = DbPassword, ConfirmPassword = DbPassword};

                 return RedirectToAction("Index", "Admin");
             }
             return View();
         }



         //Login Session for Doctors
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Login(Doctor user)
         {
             using (HospitalDbContext db = new HospitalDbContext())
             {
                 var usr = db.Doctors.Single(u => u.Username == user.Username && u.Password == user.Password);

                 if (usr != null)
                 {
                     Session["DoctorId"] = usr.DoctorId.ToString();
                     Session["Username"] = usr.Username.ToString();

                     // Redirect user to the Doctor Dashboard
                     return RedirectToAction("Index", "Doctor");

                 }
                 else
                 {
                     ModelState.AddModelError("", "Username or Password is Invalid!");
                 }
             }
             return View();
         }


         //Login Session for Nurse
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Login(Nurse user)
         {
             using (HospitalDbContext db = new HospitalDbContext())
             {
                 var usr = db.Nurses.Single(u => u.Username == user.Username && u.Password == user.Password);

                 if (usr != null)
                 {
                     Session["NurseId"] = usr.NurseId.ToString();
                     Session["Username"] = usr.Username.ToString();

                     // Redirect user to the Nurse Dashboard
                     return RedirectToAction("Index", "Nurse");

                 }
                 else
                 {
                     ModelState.AddModelError("", "Username or Password is Invalid!");
                 }
             }
             return View();
         }


         //Login Session for Laboratory
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Login(Laboratory user)
         {
             using (HospitalDbContext db = new HospitalDbContext())
             {
                 var usr = db.Laboratories.Single(u => u.Username == user.Username && u.Password == user.Password);

                 if (usr != null)
                 {
                     Session["LaboratoryId"] = usr.LaboratoryId.ToString();
                     Session["Username"] = usr.Username.ToString();

                     // Redirect user to the Laboratory Dashboard
                     return RedirectToAction("Index", "Laboratory");

                 }
                 else
                 {
                     ModelState.AddModelError("", "Username or Password is Invalid!");
                 }
             }
             return View();
         }


         //Login Session for Pharmacy
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Login(Pharmacy user)
         {
             using (HospitalDbContext db = new HospitalDbContext())
             {
                 var usr = db.Pharmacies.Single(u => u.Username == user.Username && u.Password == user.Password);

                 if (usr != null)
                 {
                     Session["PharmacyId"] = usr.PharmacyId.ToString();
                     Session["Username"] = usr.Username.ToString();

                     // Redirect user to the Pharmacy Dashboard
                     return RedirectToAction("Index", "Pharmacy");

                 }
                 else
                 {
                     ModelState.AddModelError("", "Username or Password is Invalid!");
                 }
             }
             return View();
         }


         //Login Session for Accounting
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Login(Accounting user)
         {
             using (HospitalDbContext db = new HospitalDbContext())
             {
                 var usr = db.Accountings.Single(u => u.Username == user.Username && u.Password == user.Password);

                 if (usr != null)
                 {
                     Session["AccountingId"] = usr.AccountingId.ToString();
                     Session["Username"] = usr.Username.ToString();

                     // Redirect user to the Accounting Dashboard
                     return RedirectToAction("Index", "Accounting");

                 }
                 else
                 {
                     ModelState.AddModelError("", "Username or Password is Invalid!");
                 }
             }
             return View();
         }


         //Login Session for Reception
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Login(Reception user)
         {
             using (HospitalDbContext db = new HospitalDbContext())
             {
                 var usr = db.Receptions.Single(u => u.Username == user.Username && u.Password == user.Password);

                 if (usr != null)
                 {
                     Session["ReceptionId"] = usr.ReceptionId.ToString();
                     Session["Username"] = usr.Username.ToString();

                     // Redirect user to the Reception Dashboard
                     return RedirectToAction("Index", "Reception");

                 }
                 else
                 {
                     ModelState.AddModelError("", "Username or Password is Invalid!");
                 }
             }
             return View();
         }*/


       /* //Login Session for Patients
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(PatientAccount user)
        {
            using (HospitalDbContext db = new HospitalDbContext())
            {
                var usr = db.PatientAccounts.Single(u => u.Username == user.Username && u.Password == user.Password);

                if (usr != null)
                {
                    Session["PatientId"] = usr.PatientId.ToString();
                    Session["Username"] = usr.Username.ToString();

                    // Redirect user to the Patient Dashboard
                    return RedirectToAction("Index", "PatientAccount");

                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is Invalid!");
                }
            }
            return View();
        }*/



        // User Logout

        public ActionResult LogOut()
        {
            Session.Clear();

            return RedirectToAction("Login","Home");


        }


    }
}