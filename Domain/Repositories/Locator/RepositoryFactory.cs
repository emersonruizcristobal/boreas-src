using ERC.Framework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Locator {
    public class RepositoryFactory {
        public IBaseRepository<T> GetRepositoryFor<T>() where T : class {

            if (typeof(T).Equals(typeof(Domain.Models.User))) {
                return (IBaseRepository<T>)new Domain.Repositories.UserRepository();
            }

           
            return null;
        }

        public IBaseRepository<T> GetRepositoryFor<T>(string repositry) where T : class {

            var typeName = String.Format("{0}.{1}, {2}", "Domain.Models", repositry, "Domain");
            return null;
        }
    }
}
