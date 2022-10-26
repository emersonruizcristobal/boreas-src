using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Service.Notifications {
    public static class NotificationService {
        public static ActionResult Error(this ActionResult result, string message) {
            CreateCookieWithFlashMessage(Notifications.Error, message);
            return result;
        }

        public static ActionResult Warning(this ActionResult result, string message) {
            CreateCookieWithFlashMessage(Notifications.Warning, message);
            return result;
        }

        public static ActionResult Success(this ActionResult result, string message) {
            CreateCookieWithFlashMessage(Notifications.Success, message);
            return result;
        }

        public static ActionResult Information(this ActionResult result, string message) {
            CreateCookieWithFlashMessage(Notifications.Information, message);
            return result;
        }

        public static ActionResult Alert(this ActionResult result, string message) {
            CreateCookieWithFlashMessage(Notifications.Alert, message);
            return result;
        }

        public static ActionResult Notification(this ActionResult result, string message) {
            CreateCookieWithFlashMessage(Notifications.Notification, message);
            return result;
        }

        private static void CreateCookieWithFlashMessage(Notifications notification, string message) {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(string.Format("Flash.{0}", notification), message) { Path = "/" });
        }

        private enum Notifications {
            Alert,
            Information,
            Error,
            Warning,
            Success,
            Notification
        }
    }
}
