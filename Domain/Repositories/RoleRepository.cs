using ERC.Framework.Repository;
using Domain.Configurations;
using Domain.Models;

namespace Domain.Repositories {
    public class RoleRepository : BaseRepository<BPHDbContext, Role>, IRoleRepository {
    }

    public interface IRoleRepository : IBaseRepository<Role> {
    }
}
