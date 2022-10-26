using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Shared.ViewModels;
using Service.User;
using System.Web.ApplicationServices;
using Service;
using Service.Mail;
using Service.Notifications;
using Service.Token;
using Web.Areas.Shared.Controllers;
using Service.Attributes;
using Domain.Enums;

namespace Web.Areas.System.Controllers {
    public class UsersController : BaseController {

        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        public ActionResult Index() {
            return View(new SystemUsersIndexViewModel { 
                            Users       = new UserService().GetAllUsers(),
                            Roles       = new Service.Role.RoleService().GetAllRoles()
                        });
        }

        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        public JsonResult Edit(Guid id) {
            try {
                return Json(new UserService().GetUser(id), JsonRequestBehavior.AllowGet);
            } catch (Exception exception) {
                return Json(String.Format("Unable to retrieve user's information. The following error occured: {0}", exception.Message));
            }
        }

        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        [HttpPost]
        public JsonResult Edit(SystemUsersEditViewModel viewModel) {
            try {
                new UserService().Update(viewModel.User);
                return Json("User successfully updated");
            } catch (Exception exception) {
                return Json(String.Format("Unable to update user. The following error occured: {0}", exception.Message));
            }
        }

        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        [HttpPost]
        public JsonResult Create(SystemUsersCreateViewModel viewModel) {
            try {
                new UserService().Save(viewModel.User);
                return Json("User successfully created");
            } catch (Exception exception) {
                return Json(String.Format("Unable to create user. The following error occured: {0}", exception.Message));
            }
        }

        [HttpPost]
        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        public JsonResult Save(Domain.Models.User user) {
            try {
                new UserService().Save(user);
                return Json("User saved");
            } catch (Exception exception) {
                return Json(exception.Message);
            }

        }

        [HttpGet]
        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        public JsonResult GetUser(Guid userId) {

            try {
                return Json(new AdministratorEmployeeViewModel {
                    User = new UserService().GetUser(userId)
                }, JsonRequestBehavior.AllowGet);
            } catch (Exception exception) {
                return Json(string.Format("{0}", exception.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        [HttpPost]
        public JsonResult Deactivate(Guid id) {
            try {
                new UserService().Deactivate(id);
                return Json("User successfully deactivated");
            } catch (Exception exception) {
                return Json(String.Format("Unable to deactivate user. The following error occured: {0}", exception.Message));
            }
        }

        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        [HttpPost]
        public JsonResult InitiatePasswordReset(SystemUsersDetailsViewModel viewModel) {
            try {
                new MailService().InitiatePasswordReset(viewModel.User);
                return Json(String.Format("Email sent to {0} for password reset", viewModel.User.Fullname));
            } catch (Exception exception) {
                return Json(String.Format("The following error occured: {0}", exception.Message));
            }
        }

        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        public ActionResult PasswordReset(Guid id) {
            try {
                return View(new SystemUsersPasswordResetViewModel {
                    User = new Domain.Models.User {
                        Id = new TokenService().ValidateToken(id)
                    }
                });
            } catch (Exception exception) {
                return View().Error(exception.Message);
            }
        }

        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        [HttpPost]
        public ActionResult UpdatePassword(SystemUsersPasswordResetViewModel viewModel) {

            try {
                new UserService().UpdatePassword(CurrentUser(), viewModel.NewPassword, viewModel.ConfirmNewPassword, viewModel.OldPassword);
                return Json("Your password is now updated");
            } catch (Exception exception) {
                return Json(String.Format("The following error occured: {0}", exception.Message));
            }
        }

        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        public ActionResult PasswordResetSuccessful() {
            return View();
        }

    }
}