using Domain.Models;
using Service.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Notifications;
using Service.Persistence;
using Web.Areas.Shared.ViewModels;
using Service.Attributes;
using Domain.Enums;
using Web.Areas.Shared.Controllers;
using Service.User;
using Facebook;
using System.Web.Security;

namespace Web.Areas.Account.Controllers {
    public class LoginController : BaseController {
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public JsonResult Index(AccountLoginViewModel viewModel) {

            var user = new Domain.Models.User();

            try {
                user = new AuthenticationService().Authenticate(viewModel.Email, viewModel.Password);
                UserSessionService<User>.Initialise(user);
            } catch (Exception exception) {
                if (viewModel.Password == null) {
                    return JsonError("USername/Password is incorrect");
                }
                return JsonError(exception.Message);
            }

            return Json(new LoginViewModel {
                Direction = "/Home/Main/",
                Status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Authenticate(Account account) {
            try {
                var user = new UserService().GetAuthenticatedUser(account.Username, account.Password);
                if (user != null) {
                    return Json(new ValidResponse {
                         Status = true,
                         User = user,
                    }, JsonRequestBehavior.AllowGet);
                } else {
                    return Json(new ValidResponse {
                        Status = false,
                        User = CurrentUser()
                    }, JsonRequestBehavior.AllowGet);
                }
            } catch (Exception exception) {
                return JsonError(exception.Message);
            }
        }

        public JsonResult CheckSession() {

            try {
                return Json((CurrentUser() != null), JsonRequestBehavior.AllowGet);
            } catch (Exception exception) {
                return JsonError(exception.Message);
            }

        }
    }




    public class Account {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ValidResponse {

        public Domain.Models.User User {
            get;
            set;
        }

        public bool Status {
            get;
            set;
        }

    }

    public class LoginViewModel {

        public bool Status {
            get;
            set;
        }

        public string Direction {
            get;
            set;
        }

    }
}