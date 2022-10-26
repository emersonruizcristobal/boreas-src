using ERC.Framework.Security;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;

namespace Service.User {
    public class UserService : BaseService<UserRepository> {

        public bool MatchUserRole(Guid userId, string rolename) {
            return Repository().AllIncluding(a => a.Role)
                               .Any(a => a.Id == userId && a.Role.Name == rolename);
        }


        public Domain.Models.User GetAuthenticatedUser(string name, string password) {
            var passwordHash    = new CryptographyHelper().CreateHash(password);
            return Repository().AllIncluding(a => a.Role)
                               .Where(a => a.Username == name &&
                                            a.Password == passwordHash)
                               .FirstOrDefault();
        }

        public Domain.Models.User GetUser(Guid id) {
            return Repository().Find(id);
        }

        public List<Domain.Models.User> GetAllUsers() {
            return Repository().AllIncluding(a => a.Role)
                                       .ToList();
        }

        public void Deactivate(Guid id) {
            if (id == null)
                throw new ArgumentNullException("id");

            var user    = Repository().Find(id);
            user.Tag    = Domain.Models.UserStatus.InActive;

            Repository().Edit(user);
            Repository().Save();
        }

        public void Save(Domain.Models.User user) {
            if (user == null)
                throw new ArgumentNullException("user");

            if (user.RoleId == null)
                throw new Exception("RoleId cannot be empty");

            user.Id         = Guid.NewGuid();
            user.Password   = new CryptographyHelper().CreateHash(user.Password);
            Repository().Add(user);
            Repository().Save();
        }

        public void Update(Domain.Models.User user) {
            if (user == null)
                throw new ArgumentNullException("user");

            if (user.RoleId == null)
                throw new Exception("RoleId cannot be empty");


            if (user.Password == null) {
                var oldUser = Repository().Find(user.Id);
                user.Password = oldUser.Password;
            } else {
                user.Password = new CryptographyHelper().CreateHash(user.Password);
            }


            Repository().Edit(user);
            Repository().Save();
        }

        public void UpdatePassword(Guid id, string password) {
             var user = Repository().Find(id);
            if (user == null)
                throw new Exception("Cannot update password. Unable to find user.");

            user.Password = password;
            Repository().Edit(user);
            Repository().Save();       
        }


        public void UpdatePassword(Domain.Models.User user, string newPassword, string confirmNewPassword, string oldPassword) {

            if (confirmNewPassword != newPassword) {
                throw new Exception("Please confirm new password.");
            }

            if (user == null) {
                throw new Exception("Cannot update password. Unable to find user.");
            }

            var hashedPassword = new CryptographyHelper().CreateHash(oldPassword);

            var cUser = Repository().All()
                                    .Where(a => a.Password == hashedPassword)
                                    .FirstOrDefault();

            if (cUser == null) {
                throw new Exception("Cannot update password. Invalid.");
            }

            user.Password = new CryptographyHelper().CreateHash(newPassword);
            Repository().Edit(user);
            Repository().Save();
                        

        }
    }
}
