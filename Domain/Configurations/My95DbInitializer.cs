using ERC.Framework.Security;
using Domain.Configurations;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configurations {
    public class My95DbInitializer : DropCreateDatabaseIfModelChanges<My95DbContext> {
        protected override void Seed(My95DbContext context) {

            #region settings

            var settingRepository = new SettingRepository();
            new List<Setting> { 
                new Setting {
                    Name  = "ImagePath",
                    Value = "C:/inetpub/wwwroot/wbb.olongapo/Content/img/pictures/",
                    Group = "Path"
                }
            }.ForEach(a => settingRepository.Add(a));
            settingRepository.Save();

            #endregion

            #region Users

            var defaultPassword = "baRRio215";

            var role0 = Guid.NewGuid(); //astronaut
            var role1 = Guid.NewGuid(); //admin
            var role2 = Guid.NewGuid(); //others

            var user1 = Guid.NewGuid();

            var userRepository = new UserRepository();

            var users = new List<User> { 
                new User{
                    Id          = Guid.NewGuid(),
                    Fullname    = "Emerson Cristobal",
                    Firstname   = "Emerson",
                    Middlename  = "Ruiz",
                    Lastname    = "Cristobal",
                    Position    = "Administrator",
                    Username    = "ecristobal@barrio.ph",
                    Password    = new CryptographyHelper().CreateHash(defaultPassword),
                    CreatedAt   = DateTime.Now,
                    Role        = new Role{
                                    Id          = role0,
                                    Name        = "Astronaut",
                                    RoleType    = Enums.RoleType.Astronaut,
                                }
                    
                }
            };

            users.ForEach(a => userRepository.Add(a));
            userRepository.Save();
            #endregion

            #region RoleTemplate
            var roleTemplateRepository = new RoleTemplateRepository();

            System.Enum.GetValues(typeof(ApplicationElement))
                       .Cast<ApplicationElement>()
                       .ToList()
                       .ForEach(a => {
                                        roleTemplateRepository.Add(new RoleTemplate { 
                                                                      Id                  = Guid.NewGuid(),
                                                                      ApplicationElement  = a,
                                                                      RoleId              = role0
                                                                });
                                     });

            roleTemplateRepository.Save();

            
            System.Enum.GetValues(typeof(ApplicationElement))
                       .Cast<ApplicationElement>()
                       .Where(a => a.ToString().Contains("ReadAgenda")      ||
                                   a.ToString().Contains("ReadCalendar")    ||
                                   a.ToString().Contains("ReadMeasures")    ||
                                   a.ToString().Contains("SaveMeasures")    ||
                                   a.ToString().Contains("ReadSession")     ||
                                   a.ToString().Contains("UpdateMeasures")  || 
                                   a.ToString().Contains("ReadArchive")     ||
                                   a.ToString().Contains("ReadChat")        || 
                                   a.ToString().Contains("SaveChat")        || 
                                   a.ToString().Contains("UpdateChat")      || 
                                   a.ToString().Contains("DeleteChat")      || 
                                   a.ToString().Contains("StationFour"))
                       .ToList()
                       .ForEach(a => {
                                        roleTemplateRepository.Add(new RoleTemplate { 
                                                                      Id                  = Guid.NewGuid(),
                                                                      ApplicationElement  = a,
                                                                      RoleId              = role1 //administrator
                                                                });
                                     });

            roleTemplateRepository.Save();

            
            System.Enum.GetValues(typeof(ApplicationElement))
                       .Cast<ApplicationElement>()
                       .Where(a => a.ToString().Contains("ReadAgenda")      ||
                                   a.ToString().Contains("ReadCalendar")    ||
                                   a.ToString().Contains("ReadMeasures")    ||
                                   a.ToString().Contains("SaveMeasures")    ||
                                   a.ToString().Contains("ReadSession")     ||
                                   a.ToString().Contains("UpdateMeasures")  || 
                                   a.ToString().Contains("ReadArchive")     ||
                                   a.ToString().Contains("ReadChat")        || 
                                   a.ToString().Contains("SaveChat")        || 
                                   a.ToString().Contains("UpdateChat")      || 
                                   a.ToString().Contains("DeleteChat")      || 
                                   a.ToString().Contains("StationFour"))
                       .ToList()
                       .ForEach(a => {
                                        roleTemplateRepository.Add(new RoleTemplate { 
                                                                      Id                  = Guid.NewGuid(),
                                                                      ApplicationElement  = a,
                                                                      RoleId              = role2 //doctor
                                                                });
                                     });

            roleTemplateRepository.Save();

            


            #endregion



        }
    }
}
