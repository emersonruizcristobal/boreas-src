using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web;
using System.Web.Security;

namespace Service.Persistence {

    public class UserSessionService<T> where T : BaseModel<Domain.Models.UserStatus> {

        public static T CurrentUser {
            get {
                return (T)HttpContext.Current.Session["CurrentUser"];
            }
            set {
                HttpContext.Current.Session["CurrentUser"] = value;
            }
        }

        private static IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.Current.Request.GetOwinContext().Authentication;
            }
        }

        public static void Initialise(T currentUser) {
            if (currentUser == null)
                throw new Exception("Username/Password is incorrect.");

            AuthenticationManager.SignIn(new ClaimsIdentity(new List<Claim> {
                                            new Claim("Sid", currentUser.Id.ToString()),
                                            new Claim(ClaimTypes.NameIdentifier, AntiForgeryConfig.UniqueClaimTypeIdentifier)
                                        }, DefaultAuthenticationTypes.ApplicationCookie));

            CurrentUser = currentUser;
        }

        public static void Reload(T user) {
            CurrentUser = user;
        }

        public static void End() {
            CurrentUser = null;
            FormsAuthentication.SignOut();
        }
    }
}
