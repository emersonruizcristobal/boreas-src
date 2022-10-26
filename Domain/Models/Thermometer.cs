using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models {
    public class Thermometer : BaseModel<ThermometerState> {

        public float Celsius {
            get;
            set;
        }

        public float Fahrenheit {
            get;
            set;
        }

        public float Floor {
            get;
            set;
        }
        public float Ceiling {
            get;
            set;
        }

        public string Name {
            get;
            set;
        }

        public int Step {
            get;
            set;
        }

    }

    public enum ThermometerState {
        Active,
        Inactive
    }
}
