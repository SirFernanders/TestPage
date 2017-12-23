using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Test.Models;
using System.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Controllers
{
    public class ContactController : Controller
    {
        // GET: /<controller>/
        public ActionResult Sent()
        {
            return View();
        }

        public IActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("\n \n at indexs \n \n");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("fernandomontes91@me.com"));  // replace with valid value 
                message.From = new MailAddress("personalsiteemailer@gmail.com");  // replace with valid value
                message.Subject = "WebSite Email";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "personalsiteemailer@gmail.com",  // replace with valid value
                        Password = "TUv-49R-eL5-zcP"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    System.Diagnostics.Debug.WriteLine("I am here so far.");
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }


    }
}
