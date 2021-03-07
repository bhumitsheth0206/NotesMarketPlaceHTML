using NotesMarketPlace.Context;
using NotesMarketPlace.Helper;
using NotesMarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Notes_MarketPlace.Controllers
{
    public class ForgotPasswordController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [Route("Forgotpassword")]
        // GET: ForgotPassword

        [HttpGet]
        public ActionResult Forgotpassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePassword(forgotpassword model)
        {
            bool isValid = dobj.Users.Any(x => x.EmailID == model.EmailID);

            if(isValid)
            {
                Users obj = dobj.Users.Where(x => x.EmailID == model.EmailID).FirstOrDefault();
                if(obj.IsEmailVerified)
                {
                    SendVerificationLinkEmail(obj.EmailID);
                    return RedirectToAction("Login", "Login");
                }
            }
            
            else
            {
                TempData["Error"] = "Email  Address is not registered";
                return RedirectToAction("Forgotpassword", "ForgotPassword");
            }
            return View();
        }

        public void SendVerificationLinkEmail(string EmailID)
        {

            Users obj = dobj.Users.Where(x => x.EmailID == EmailID).FirstOrDefault();
            if(obj != null)
            {
                string strNewPassword = GeneratePassword().ToString();
                obj.Password = EncryptPassword.EncMD5(strNewPassword);
                dobj.SaveChanges();
                var fromEmail = new MailAddress("bhumitsheth02@gmail.com", "Notes-MarketPlace");
                var toEmail = new MailAddress(EmailID);
                var fromEmailPassword = "*******"; // Replace with actual password
                string subject = "Notes MarketPlace- Email Verification";

                string body = "<br/><br/>Hello" +
                    "<br/><br/>We have generated a new password for you" +
                    "<br/><br/>Password:"+ strNewPassword;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                };

                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                smtp.Send(message);
            }

            
        }

        //For generating random number
        public string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0,";
            allowedChars += "@,$,!,%,*,#,?,&,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string IDString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;
            }
            return NewPassword;
        }


    }
}