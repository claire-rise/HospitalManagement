using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net.Mail;
using MySmtpClassLib;
using hospitalwebapp.Models;

namespace hospitalwebapp.Controllers
{
    public class PatientController : Controller
    {
        
        //Get
        //View A Single Patient
        
        public ActionResult PatientProfile(int? id)
        {

            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                    /* return RedirectToAction("Login", "Account");*/
                }

                //Search for the id in the collection 

                if (Session["PatientId"] != null)
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
                    return RedirectToAction("PatientLogin", "Account");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("PatientLogin", "Account");
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

            if (Session["PatientId"] != null)
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
                return RedirectToAction("PatientLogin", "Account");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //Edit Staff
        public ActionResult EditPatient(PatientAccount patient)
        {
            try
            {
                if (Session["PatientId"] != null)
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
                        return RedirectToAction($"PatientProfile/{Session["PatientId"]}", "Patient");
                    }
                }
                else
                {
                    return RedirectToAction("PatientLogin", "Account");
                }




            }
            catch (Exception)
            {

                ViewBag.Error = "Invalid Patient Id! Changes was not successful.";
            }
            return RedirectToAction("PatientLogin", "Account");
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


        //GET
        public ActionResult Contact()
        {
            ViewBag.Title = "Our contact";

            return View();
        }

        //PoST
        [HttpPost]
        public ActionResult Contact(EmailModel email, HttpPostedFileBase UploadFile)
        {

            try
            {
                if(Session["PatientId"] != null)
                {
                    String MessageBody = email.Message;

                    using (MailMessage client = new MailMessage())
                    {
                        client.From = new MailAddress(Connection.SenderEmail, Connection.SenderName);
                        /*client.To.Add(email.UserEmail);*/
                        client.To.Add("olutayo_4care.prof@yahoo.com");
                        client.Subject = Connection.MailSubject;
                        client.Body = "You have a message from <br><b>Fullname:</b>" +
                                email.Fullname + "<br><b>Email:</b>" + email.UserEmail +
                                "<br><br><br><b>Message:</b>" + MessageBody;
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
                else
                {
                    return RedirectToAction("PatientLogin", "Account");
                }
               
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

    }
}