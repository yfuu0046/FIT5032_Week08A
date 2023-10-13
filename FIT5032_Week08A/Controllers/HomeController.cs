using FIT5032_Week08A.Models;
using FIT5032_Week08A.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_Week08A.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Please comment out these codes once you have registered your API key.
            //EmailSender es = new EmailSender();
            //es.RegisterAPIKey();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        public ActionResult LoadDynamicContent()
        {
            // 這裡可以返回動態載入的內容，可以是一段 HTML 代碼或者一個部分視圖
            return PartialView("_DynamicContent");
        }

        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "The 'model' parameter cannot be null.");
                }

                if (model.Attachment == null)
                {
                    throw new ArgumentNullException(nameof(model.Attachment), "No attachment provided.");
                }

                if (ModelState.IsValid)
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    HttpPostedFileBase attachment = model.Attachment;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents, attachment);

                    ViewBag.Result = "Email has been sent.";
                    ModelState.Clear();
                    return View(new SendEmailViewModel());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Result = "An error occurred while sending the email. Error message: " + ex.Message;
            }

            return View();
        }


    }

}




    