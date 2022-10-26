using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models {

    public class BaseModel<T> where T : struct, IComparable, IFormattable, IConvertible {

        public BaseModel() {
            this.CreatedAt  = DateTime.Now;
            this.Id         = Guid.NewGuid();
        }
        

        public Guid Id {
            get;
            set;
        }

        public DateTime CreatedAt {
            get;
            set;
        }

        public DateTime? UpdatedAt {
            get;
            set;
        }

        public T Tag {
            get;
            set;
        }

        public int Order {
            get;
            set;
        }

    }
}
