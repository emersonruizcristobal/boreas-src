using ERC.Framework.Repository;
using Domain.Configurations;
using Domain.Models;

namespace Domain.Repositories {
    public class UserRepository : BaseRepository<BPHDbContext, User>, IUserRepository {
    }

    public interface IUserRepository : IBaseRepository<User> {
    }
}
