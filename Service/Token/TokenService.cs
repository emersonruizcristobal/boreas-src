using Domain.Repositories;
using Service;
using System;

namespace Service.Token {
    public class TokenService : BaseService<TokenRepository> {

        public Guid Create(Guid userId) {
            var token = new Domain.Models.Token { 
                                            Id      = Guid.NewGuid(),
                                            UserId  = userId
                                       };
            Repository().Add(token);
            Repository().Save();
            return token.Id;
        }

        public Guid ValidateToken(Guid id) {
            var token = Repository().Find(id);
            if (token == null)
                throw new Exception("Unable to find activation code");

            if(token.CreatedAt < DateTime.Now.AddHours(-24))
                throw new Exception("Token expired");

            return token.UserId;
        }

    }
}
