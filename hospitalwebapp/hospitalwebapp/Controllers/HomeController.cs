using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using MySmtpClassLib;
using hospitalwebapp.Models;

namespace hospitalwebapp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About us";

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
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            ViewBag.Title = "Forgot Password";
            return View();
        }

       
           
    }

   

}

