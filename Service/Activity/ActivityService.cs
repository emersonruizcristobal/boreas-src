using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Activity {
    public class ActivityService : BaseService<ActivityRepository> {

        public List<Domain.Models.Activity> GetAll(Domain.Models.ActivityEntity type) {

            var entities = Repository().All()
                                       .Where(a => a.Tag == type)
                                       .ToList();

            return entities;

        }

        public Domain.Models.Activity Get(Guid id) {

            var entity = Repository().All()
                                       .Where(a => a.Id == id)
                                       .FirstOrDefault();

            return entity;

        }

        public void Delete(Guid entityId) {

            if (!Repository().All().Any(a => a.Id == entityId)) {
                throw new ArgumentNullException("activity");
            }

            Repository().Delete(entityId);
            Repository().Save();

        }

        public Domain.Models.Activity Save(Domain.Models.Activity entity) {


            var existingItem = Repository().Find(entity.Id);

            if (existingItem == null) {
                Repository().Add(entity);
                Repository().Save();
            } else {
                Repository().Edit(entity);
                Repository().Save();
            }


            return entity;

        }

    }
}
