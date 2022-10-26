using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Thermometer {
    public class ThermometerService : BaseService<Domain.Models.Thermometer, Domain.Repositories.ThermometerRepository> {

        public override void Save(Domain.Models.Thermometer entity) {
            var existingEntity = base.GetAllBy(a => a.Name == entity.Name).FirstOrDefault();

            if(existingEntity == null) {
                base.Save(entity);
            } else {
                existingEntity.Fahrenheit   = entity.Fahrenheit;
                existingEntity.Celsius      = entity.Celsius;
                existingEntity.Step         = entity.Step;
                base.Update(existingEntity);
            }
        }

    }
}
