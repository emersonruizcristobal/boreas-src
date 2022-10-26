using ERC.Framework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Locator {
    public class RepositoryLocator {
        public IBaseRepository<T> GetRepositoryFor<T>() where T : class {

            //Get the name of the type
            var type    = typeof(T).Name;
            var suffix  = "Repository";

            //Create an instance of the corresponding form using reflection
            return (IBaseRepository<T>)Activator.CreateInstance(Type.GetType(String.Format("{0}.{1}{2}, {3}",
                                                                       "Repositories.Domain.Repositories",
                                                                       type,
                                                                       suffix,
                                                                       "Domain"), true));
        }

        public IBaseRepository<T> GetRepositoryFor<T>(string repositoryName) where T : class {

            if (repositoryName == null)
                throw new ArgumentNullException("name");

            Type repositoryType = Type.GetType(String.Format("{0}.{1}{2}, {3}", "Domain.Repositories", repositoryName, "Repository", "Web.Domain"));
            return (IBaseRepository<T>)Activator.CreateInstance(repositoryType);
        }
    }
}
