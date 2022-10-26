using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configurations {
    public class My95DbContext : DbContext {

        public My95DbContext() : base("DefaultConnection") { 
        }

        public DbSet<Role> Roles {
            set;
            get;
        }

        public DbSet<RoleTemplate> RoleTemplates {
            set;
            get;
        }

        public DbSet<Token> Tokens {
            set;
            get;
        }

        public DbSet<User> Users {
            set;
            get;
        }

        public DbSet<ControlNumber> ControlNumbers {
            get;
            set;
        }


        public DbSet<Setting> Settings {
            get;
            set;
        }
        

    }
}
