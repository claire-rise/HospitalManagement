using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net.Mail;
using MySmtpClassLib;
using hospitalwebapp.Models;
using Microsoft.Ajax.Utilities;

namespace hospitalwebapp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Staff Account
        public ActionResult Index()
        {
            try
            {
                // Show a list of all registered Staffs
                using (HospitalDbContext db = new HospitalDbContext())
                {


                    if (Session["StaffId"] != null)
                    {
                        var user = db.StaffAccounts.ToList();

                        return View(user);
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    
                }
            }
            catch (Exception)
            {

                ViewBag.Error = $"Access Denied! Invalid Request.";
            }

            return View();
        }

        // GET: Patient Account

        public ActionResult HospitalPatients()
        {
            // Show a list of all registered Patients
            using (HospitalDbContext db = new HospitalDbContext())
            {
                if (Session["StaffId"] != null)
                {
                    var user = db.PatientAccounts.ToList();

                    return View(user);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
               
            }
        }


        //GET
        public ActionResult Feedback()
        {
            ViewBag.Title = "Feedback";

            return View();
        }

        //create a delete method
        //-----------------------------------------------------------
        //                      DELETE METHOD
        //-----------------------------------------------------------
        public void DeleteImage(String path)
        {
            var PathToDelete = Server.MapPath("/") + path;

            if (System.IO.File.Exists(PathToDelete))
            {
                //delete the file in the path if file exists in the path

                System.IO.File.Delete(PathToDelete);
            }

        }

        //POST
        [HttpPost]
        public ActionResult Feedback(EmailModel email, HttpPostedFileBase UploadFile)
        {

            try
            {

                String MessageBody = email.Message;

                using (MailMessage client = new MailMessage())
                {
                    client.From = new MailAddress(Connection.SenderEmail, Connection.SenderName);
                    client.To.Add(email.UserEmail);
                    client.Subject = Connection.MailSubject;
                    client.Body = MessageBody;
                    client.IsBodyHtml = true;
                    client.Priority = MailPriority.High;

                    //Check for File Uploads
                    if (UploadFile != null)
                    {
                        var path = Server.MapPath("/") + "Content/uploads" + UploadFile.FileName;

                        //Save image in the path

                        UploadFile.SaveAs(path);
                        client.Attachments.Add(new Attachment(path));
                    }

                    //Call the SMTP method we created in the class Library
                    Connection.MySmtp(client);

                }//Using closed here


                //Delete file from server but first check if the file was uploaded, otherwise ignore
                if (UploadFile != null)
                {
                    DeleteImage("Content/uploads" + UploadFile.FileName);
                }


                ViewBag.Feedback = "Message sent successfully.";
                //Clear control
                ModelState.Clear();

                return View();
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }//End of Feedback




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
            var SelectedRegType = "";
            if (ModelState.IsValid)
            {
                using (HospitalDbContext db = new HospitalDbContext())
                {
                    //Capture the selected Department from the user's input 
                    SelectedRegType = account.PatientRegistrationType.ToString();

                    //Check if Username and Email already exist in the database. This allows to prevent
                    //Double registration 

                    PatientAccount searchObjectUsername = db.PatientAccounts.FirstOrDefault(c => c.Username == account.Username.ToString());

                    PatientAccount searchObjectEmail = db.PatientAccounts.FirstOrDefault(c => c.Email == account.Email.ToString());




                    //If Username or Email already Exists, throw a message
                    if (searchObjectUsername == null && searchObjectEmail == null)
                    {
                        db.PatientAccounts.Add(account);
                        db.SaveChanges();

                        // Clear Controls 
                        ModelState.Clear();
                        // Display notification to the user
                        ViewBag.Message = ($"{account.LastName} {account.FirstName}  Successfully Registered as Patient: {SelectedRegType}!");

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
        //View A Single Staff
        public ActionResult StaffProfile(int? id)
        {

            try
            {
                if (id == null)
                {
                    /*return HttpNotFound();*/
                    return RedirectToAction("Login", "Account");
                }

                //Search for the id in the collection 

                if (Session["StaffId"] != null)
                {
                    using (HospitalDbContext db = new HospitalDbContext())
                    {
                        var staffAccount = db.StaffAccounts
                            .Where(m => m.StaffId == id)
                            .FirstOrDefault();

                       
                        return View(staffAccount);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception)
            {
                
                return RedirectToAction("Login", "Account"); 
            }
        }


        //Edit Staff
        public ActionResult EditStaff(int? id)
        {
            //Search for the id in the collection 

            if (id == null)
            {
                return HttpNotFound();
            }

            if(Session["StaffId"] != null)
            {
                using (HospitalDbContext db = new HospitalDbContext())
                {
                    var staff = db.StaffAccounts
                        .Where(row => row.StaffId == id)
                        .FirstOrDefault();

                    return View(staff);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //Edit Staff
        public ActionResult EditStaff(StaffAccount staffAccount)
        {
            try
            {
                if(Session["StaffId"] != null)
                {
                    //Search for the id in the collection 

                    using (HospitalDbContext db = new HospitalDbContext())
                    {
                        if (staffAccount.StaffId > 0)
                        {
                            //Edit
                            var staff = db.StaffAccounts
                                .Where(m => m.StaffId == staffAccount.StaffId)
                                .FirstOrDefault();
                            if (staff != null)
                            {
                                staff.FirstName = staffAccount.FirstName;
                                staff.LastName = staffAccount.LastName;
                                staff.StaffGender = staffAccount.StaffGender;
                                staff.Profession = staffAccount.Profession;
                                staff.StaffDepartment = staffAccount.StaffDepartment;
                                staff.DateOfBirth = staffAccount.DateOfBirth;
                                staff.Email = staffAccount.Email;
                                staff.Address = staffAccount.Address;
                                staff.Phone = staffAccount.Phone;
                                staff.Username = staffAccount.Username;
                                staff.Password = staffAccount.Password;
                                staff.ConfirmPassword = staffAccount.ConfirmPassword;
                            }

                        }
                        else
                        {
                            //Save
                            db.StaffAccounts.Add(staffAccount);

                        }

                        db.SaveChanges();
                        return RedirectToAction("Index", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }

               
                
        
            }
            catch (Exception)
            {
                
                ViewBag.Error = "Invalid Staff Id! Changes was not successful.";
            }
            return View();
        }


        // GET: Delete Staff in StaffAccount database
        public ActionResult DeleteStaff(int? id)
        {
            try
            {
                //Search for the id in the collection 

                if (id == null)
                {
                    return HttpNotFound();
                }

                if (Session["StaffId"] != null)
                {
                    using (HospitalDbContext db = new HospitalDbContext())
                    {
                        var staff = db.StaffAccounts
                            .Where(row => row.StaffId == id)
                            .FirstOrDefault();

                        return View(staff);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: StaffAccounts/Delete/5
        [HttpPost, ActionName("DeleteStaff")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmedDeleteStaff(int id)
        {
            try
            {
                if (Session["StaffId"] != null)
                {
                    using (HospitalDbContext db = new HospitalDbContext())
                    {
                        var staff = db.StaffAccounts
                                .Where(row => row.StaffId == id)
                                .FirstOrDefault();

                        db.StaffAccounts.Remove(staff);
                        db.SaveChanges();

                        return RedirectToAction("Index", "Account");
                    }

                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Login", "Account");
            }
            
        }

       


        //Get
        //View A Single Patient
        public ActionResult PatientProfile(int? id)
        {

            try
            {
                if (id == null)
                {
                    /*return HttpNotFound();*/
                    return RedirectToAction("Login", "Account");
                }

                //Search for the id in the collection 

                if (Session["StaffId"] != null || Session["PatientId"] != null)
                {
                    using (HospitalDbContext db = new HospitalDbContext())
                    {
                        var patient = db.PatientAccounts
                            .Where(m => m.PatientId == id)
                            .FirstOrDefault();


                        return View(patient);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Login", "Account");
            }
        }


        //Edit Patient
        public ActionResult EditPatient(int? id)
        {
            //Search for the id in the collection 

            if (id == null)
            {
                return HttpNotFound();
            }

            if (Session["PatientId"] != null || Session["StaffId"] != null)
            {
                using (HospitalDbContext db = new HospitalDbContext())
                {
                    var patient = db.PatientAccounts
                        .Where(row => row.PatientId == id)
                        .FirstOrDefault();

                    return View(patient);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //Edit Staff
        public ActionResult EditPatient(PatientAccount patient)
        {
            try
            {
                if (Session["PatientId"] != null || Session["StaffId"] != null)
                {
                    //Search for the id in the collection 

                    using (HospitalDbContext db = new HospitalDbContext())
                    {
                        if (patient.PatientId > 0)
                        {
                            //Edit
                            var user = db.PatientAccounts
                                .Where(m => m.PatientId == patient.PatientId)
                                .FirstOrDefault();
                            if (user != null)
                            {
                                user.FirstName = patient.FirstName;
                                user.LastName = patient.LastName;
                                user.PatientGender = patient.PatientGender;
                                user.Profession = patient.Profession;
                                user.Address = patient.Address;
                                user.DateOfBirth = patient.DateOfBirth;
                                user.Email = patient.Email;
                                user.Phone = patient.Phone;
                                user.Username = patient.Username;
                                user.PatientRegistrationType = patient.PatientRegistrationType;
                                user.Password = patient.Password;
                                user.ConfirmPassword = patient.ConfirmPassword;
                            }

                            
                        }
                        else
                        {
                            //Save
                            db.PatientAccounts.Add(patient);

                        }

                        db.SaveChanges();
                        return RedirectToAction("HospitalPatients", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }




            }
            catch (Exception)
            {

                ViewBag.Error = "Invalid Patient Id! Changes was not successful.";
            }
            return RedirectToAction("Login", "Account");
        }


        // GET: Delete Patient in PatientAccount database. Note a patient is not granted access to perform this role
        public ActionResult DeletePatientRecord(int? id)
        {
            try
            {
                //Search for the id in the collection 

                if (id == null)
                {
                    return HttpNotFound();
                }

                if (Session["StaffId"] != null)
                {
                    using (HospitalDbContext db = new HospitalDbContext())
                    {
                        var patient = db.PatientAccounts
                            .Where(row => row.PatientId == id)
                            .FirstOrDefault();

                        return View(patient);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: StaffAccounts/Delete/5
        [HttpPost, ActionName("DeletePatientRecord")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmedDeletePatientRecord(int id)
        {
            try
            {
                if (Session["StaffId"] != null)
                {
                    using (HospitalDbContext db = new HospitalDbContext())
                    {
                        var patient = db.PatientAccounts
                                .Where(row => row.PatientId == id)
                                .FirstOrDefault();

                        db.PatientAccounts.Remove(patient);
                        db.SaveChanges();

                        return RedirectToAction("HospitalPatients", "Account");
                    }

                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Login", "Account");
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
                        Session["StaffDepartment"] = usr.StaffDepartment.ToString();

                        // Redirect user to their Respective Dashboards
                        //if user's department is Admin Redirect to the Admin Dashboard
                       /* if (usr.StaffDepartment.ToString().Equals("Admin"))
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
                        return RedirectToAction("Registration", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or Password is Invalid!");

                        return View();
                    }
                }

            }
            catch (Exception)
            {
                ViewBag.Error = $"Either username or Password is Invalid";

                return View();
            }


        }


        //Get
        public ActionResult PatientLogin()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientLogin(PatientAccount user)
        {

            try
            {
                using (HospitalDbContext db = new HospitalDbContext())
                {
                    var patient = db.PatientAccounts.Single(u => u.Username == user.Username && u.Password == user.Password);
               

                if (patient != null)
                    {
                        Session["PatientId"] = patient.PatientId.ToString();
                        Session["Username"] = patient.Username.ToString();
                        Session["PatientRegistrationType"] = patient.PatientRegistrationType.ToString();


                    return RedirectToAction($"PatientProfile/{Session["PatientId"]}", "Patient");
                }

              
                else
                    {
                        ModelState.AddModelError("", "Username or Password is Invalid!");

                        return View();
                    }
                }

            }
            catch (Exception)
            {
                ViewBag.Error = $"Either username or Password is Invalid";

                return View();
            }


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

            return RedirectToAction("Login");


        }


    }
}