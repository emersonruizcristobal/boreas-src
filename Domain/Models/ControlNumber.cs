using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models {
    public class ControlNumber : BaseModel<ControlNumberState> {



        public int SequenceNumber {
            get;
            set;
        }

        //barangay abbreviation
        public string Prefix {
            get;
            set;
        }

        public string TransactionCode {
            get;
            set;
        }


    }

    public enum ControlNumberState{
        Active,
        Inactive
    }
}
