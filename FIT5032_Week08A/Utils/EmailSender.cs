using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FIT5032_Week08A.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public async Task Send(String toEmail, String subject, String contents, HttpPostedFileBase postedFile)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("arvinfu99@outlook.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            if (postedFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    postedFile.InputStream.CopyTo(memoryStream);
                    byte[] bytes = memoryStream.ToArray();
                    Attachment attachment = new Attachment();
                    attachment.Content = Convert.ToBase64String(bytes);
                    attachment.Filename = postedFile.FileName;
                    msg.AddAttachment(attachment.Filename, attachment.Content);
                }
            }

            var response = await client.SendEmailAsync(msg);
        }

    }
}