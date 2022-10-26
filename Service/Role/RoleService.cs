using Domain.Repositories;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Service.Role {
    public class RoleService : BaseService<RoleRepository> {
        public IEnumerable<SelectListItem> GetAllRoles() {
            return Repository().All()
                               .Select(item => new SelectListItem {
                                   Text = item.Name,
                                   Value = item.Id.ToString()
                               });
        }

        public IEnumerable<SelectListItem> GetExternalRoles() {
            return Repository().All()
                               .Where(a => a.RoleType == Domain.Enums.RoleType.Admin)
                               .Select(item => new SelectListItem {
                                   Text = item.Name,
                                   Value = item.Id.ToString()
                               });
        }

        public List<Domain.Models.Role> GetAllRoles(bool all) {
            return Repository().All().ToList();
        }

        public Domain.Models.Role GetRole(Guid id) {
            return Repository().Find(id);
        }

        public void Update(Domain.Models.Role role) {
            if (role == null)
                throw new ArgumentNullException("role");

            if (Repository().All().Any(a => a.Id != role.Id && a.Name == role.Name))
                throw new Exception("Role name already exisits.");

            Repository().Edit(role);
            Repository().Save();
        }

        public void Save(Domain.Models.Role role) {
            if (role == null)
                throw new ArgumentNullException("role");

            if (Repository().All().Any(a => a.Name == role.Name))
                throw new Exception("Role name already exisits.");

            Repository().Add(role);
            Repository().Save();
        }


        public Domain.Models.Role GetRoleByName(string name) {
            return Repository().All()
                               .Where(a => a.Name == name)
                               .FirstOrDefault();
        }
    }
}
