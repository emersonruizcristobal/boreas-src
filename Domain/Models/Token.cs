using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models {
    public class Token : BaseModel<TokenState> {
        public Guid UserId {
            set;
            get;
        }
    }

    public enum TokenState {
        Unclaimed,
        Claimed
    }
}
