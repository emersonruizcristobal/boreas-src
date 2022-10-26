using ERC.Framework.Bootstrapper;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configurations {
    public class ApplicationInitializer {
        public ApplicationInitializer() {

            IoC.InitializeWith(new DependencyResolverFactory());
            Database.SetInitializer<BPHDbContext>(new BPHDbInitializer());
            //Database.SetInitializer<BPHDbContext>(null);
            IoC.Resolve<IUserRepository>().All().Count();
        }
    }
}
