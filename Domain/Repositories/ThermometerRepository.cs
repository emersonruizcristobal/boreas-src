using Domain.Configurations;
using Domain.Models;
using ERC.Framework.Repository;

namespace Domain.Repositories {

    public class ThermometerRepository : BaseRepository<BPHDbContext, Thermometer>, IThermometerRepository {

    }

    public interface IThermometerRepository : IBaseRepository<Thermometer> {

    }

}
