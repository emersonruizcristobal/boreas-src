using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models {
    public class Activity : BaseModel<ActivityEntity> {

        public string Description {
            get;
            set;
        }

        public Guid UserId {
            get;
            set;
        }

        public string UserName {
            get;
            set;
        }

    }

    public enum ActivityEntity {
        Agenda,
        Committee,
        Communication,
        Employee,
        Measure,
        MeasureSponsor,
        Session,
        SessionCommunication
    }
}
