using System.Web.Mvc;

namespace Web.Areas.Thermometer {
    public class ThermometerAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Thermometer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "Thermometer_default",
                "Thermometer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}