using ERC.Framework.Repository;
using Domain.Configurations;
using Domain.Models;

namespace Domain.Repositories {
    public class RoleTemplateRepository : BaseRepository<BPHDbContext, RoleTemplate>, IRoleTemplateRepository {
    }

    public interface IRoleTemplateRepository : IBaseRepository<RoleTemplate> {
    }
}
