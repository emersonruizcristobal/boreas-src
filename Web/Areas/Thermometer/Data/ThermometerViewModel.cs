using Domain.Models;
using System.Collections.Generic;

namespace Web.Areas.Thermometer.Data {
    public class ThermometerViewModel {

        public List<Domain.Models.Thermometer> Thermometers {
            get;
            set;
        }

        public Domain.Models.Thermometer Thermometer {
            get;
            set;
        }

    }
}