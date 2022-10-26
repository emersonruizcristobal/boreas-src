using Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Authentication {
    public class AuthenticationService {
        public Domain.Models.User Authenticate(string email, string password) {
            return new UserService().GetAuthenticatedUser(email, password);
        }
    }
}
