using ERC.Framework.Repository;
using Domain.Configurations;
using Domain.Models;

namespace Domain.Repositories {
    public class TokenRepository : BaseRepository<BPHDbContext, Token>, ITokenRepository {
    }

    public interface ITokenRepository : IBaseRepository<Token> {
    }
}
