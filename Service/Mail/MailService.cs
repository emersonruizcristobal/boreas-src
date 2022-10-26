using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Postal;
using System.Web.Mvc;
using System.IO;
using System.Web;
using Service.Token;

namespace Service.Mail {
    public class MailService {
        private EmailService _service;
        private HttpContext _context = HttpContext.Current;

        private EmailService Service() {
            if (_service == null) {
                var path = _context.Server.MapPath(@"~/Areas/Shared/Views/Email/");
                _service = new EmailService(new ViewEngineCollection{
                                    new FileSystemRazorViewEngine(path)
                              });
                return _service;
            } else {
                return _service;
            }
        }

        public void InitiatePasswordReset(Domain.Models.User user) {
            if (user == null)
                throw new ArgumentNullException("user");

            var url = String.Format("{0}{1}{2}{3}", "http://", 
                                                    _context.Request.Url.Authority, 
                                                    "/administration/users/password-reset/", 
                                                    new TokenService().Create(user.Id));

            var mail    = new MailTemplateInitiateReset();
            mail.To         = user.Username;
            mail.From       = "admin@erc.com";
            mail.Name       = user.Fullname;
            mail.Link       = url;
            Service().Send(mail);
        }
    }
}
