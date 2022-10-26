using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Setting {
    public class SettingService : BaseService<SettingRepository> {



        public void UpdatePrintBackgroundSettings(string value) {

            var setting = Repository().All()
                                      .Where(a => a.Name == "ShowPermitTemplateBackground")
                                      .FirstOrDefault();

            if (setting == null) {
                Repository().Add(new Domain.Models.Setting {
                     CreatedAt = DateTime.Now,
                     Group     = "Config",
                     Value     = value,
                     Name      = "ShowPermitTemplateBackground",
                     Tag       = Domain.Models.SettingStatus.Active,
                     Order     = 0
                });

                Repository().Save();
                return;
            }

            setting.Value = value;

            Repository().Edit(setting);
            Repository().Save();

        }

        public string GetValueByName(string name) {

            
            var setting = Repository().All()
                                      .Where(a => a.Name == name)
                                      .FirstOrDefault();

            if (setting == null) {
                return string.Empty;
            } else {
                return setting.Value;
            }

        }

        public Domain.Models.Setting GetPrintBackgroundSettings() {

            var setting = Repository().All()
                                      .Where(a => a.Name == "ShowPermitTemplateBackground")
                                      .FirstOrDefault();

            return setting;

        }

        public void Save(Domain.Models.Setting setting) {

            var existingItem = Repository().All()
                                           .Where(a => a.Id == setting.Id)
                                           .FirstOrDefault();

            if (existingItem == null) {

                Repository().Add(setting);
                Repository().Save();
            } else {
                Repository().Edit(setting);
                Repository().Save();
            }

        }

        public List<Domain.Models.Setting> GetAllItems() {

            var settings = Repository().All()
                                       .OrderBy(a => a.Name)
                                       .ToList();

            return settings;

        }

        public Domain.Models.Setting Edit(Guid id) {

            var setting = Repository().Find(id);

            return setting;

        }
    }
}
