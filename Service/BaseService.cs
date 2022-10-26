using Domain.Repositories;
using ERC.Framework.Repository;  
using Domain.Repositories.Locator;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ERC.Framework.Security;

namespace Service {
    public abstract class BaseService<T> where T : class, new() {

        protected string GetTimeStamp(DateTime time) {
            return time.ToString("yyM");
        }

        private static Random random = new Random();
        protected static string RandomString(int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        protected static int RandomInteger() {
            Random random = new Random();
            return random.Next(1000, 9000);
        }

        SettingRepository   _settingRepository;
        RepositoryLocator   _locator;
        T                   _repository;

        protected RepositoryLocator Locator() {
            if (_locator == null) {
                _locator = new RepositoryLocator();
                return _locator;
            } else {
                return _locator;
            }
        }

        protected T Repository() {
            if (_repository == null) {
                _repository = new T();
                return _repository;
            } else {
                return _repository;
            }
        }


        private SettingRepository RepositorySetting() {
            if (_settingRepository == null) {
                _settingRepository = new SettingRepository();
                return _settingRepository;
            } else {
                return _settingRepository;
            }
        }

        protected TValue DbSettings<TValue>(string settingName) {
            var setting = RepositorySetting().All().Where(a => a.Name == settingName).FirstOrDefault();
            if (setting == null)
                throw new Exception("Unable to find setting");

            return (TValue)Convert.ChangeType(setting.Value, typeof(TValue));
        }

        protected string DbSettings(string settingName) {
            var setting = RepositorySetting().All().Where(a => a.Name == settingName).FirstOrDefault();
            if (setting == null)
                throw new Exception("Unable to find setting");

            return setting.Value;
        }

        protected void UpdateSetting(string settingName, string value) {
            var setting = RepositorySetting().All().Where(a => a.Name == settingName).FirstOrDefault();
            if (setting == null)
                throw new Exception("Unable to find setting");

            setting.Value = value;
            setting.UpdatedAt = DateTime.Now;

            RepositorySetting().Edit(setting);
            RepositorySetting().Save();
        }

        protected TValue ConfigSettings<TValue>(string settingName) {
            return (TValue)Convert.ChangeType(ConfigurationManager.AppSettings[settingName], typeof(TValue));
        }

    }


    public abstract class BaseService<T, TR> where TR: IBaseRepository<T>, new()
                                             where T: class{

        #region Locator
        RepositoryLocator _Locator;
        protected RepositoryLocator Locator {
            get {
                if (_Locator == null) 
                    _Locator = new RepositoryLocator();
                
                return _Locator;
            }
        }
        #endregion 

        #region Repository
        private TR _Repository;
        protected TR Repository {
            get {
                if (_Repository == null) 
                    _Repository = new TR();

                return _Repository;
            }
        }


        public virtual void Save(T entity) {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Repository.Add(entity);
            Repository.Save();
        }

        public virtual T SaveAndGet(T entity) {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Repository.Add(entity);
            Repository.Save();

            return entity;
        }

        public virtual void Save(IEnumerable<T> entities) {
            if (entities == null)
                throw new ArgumentNullException("entities");

            Repository.Add(entities);
            Repository.Save();
        }


        public virtual void Save(T entity, bool saveIfNotExist) {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var idProperty = entity.GetType()
                                   .GetProperties()
                                   .Where(a => a.Name == "Id")
                                   .FirstOrDefault();

            if (idProperty == null)
                throw new Exception("Unable to find `Id` property.");

            var idValue     = (Guid) idProperty.GetValue(entity);
            var oldEntity   = Repository.Find(idValue);

            if (oldEntity == null && saveIfNotExist) 
                Repository.Add(entity);

            Repository.Save();
        }

        public virtual void Save(IEnumerable<T> entities, bool saveIfNotExist) {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var entity in entities) {
                var idProperty = entity.GetType()
                                       .GetProperties()
                                       .Where(a => a.Name == "Id")
                                       .FirstOrDefault();

                if (idProperty == null)
                    throw new Exception("Unable to find `Id` property.");

                var idValue     = (Guid)idProperty.GetValue(entity);
                var oldEntity   = Repository.Find(idValue);


                if (oldEntity == null && saveIfNotExist)
                    Repository.Add(entity);
            }
            Repository.Save();
        }

        public virtual T UpdateAndGet(T entity) {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Repository.Edit(entity);
            Repository.Save();

            return entity;
        }

        public virtual void Update(T entity) {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Repository.Edit(entity);
            Repository.Save();
        }

        public virtual void Update(IEnumerable<T> entity) {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Repository.Edit(entity);
            Repository.Save();
        }

        public virtual void Delete(Guid id) {
            Repository.Delete(id);
            Repository.Save();
        }

        public virtual void Delete(IEnumerable<Guid> ids) {
            foreach (var id in ids)
                Repository.Delete(id);
            Repository.Save();
        }

        public virtual void Delete(Expression<Func<T, bool>> where) {
            var entities = Repository.All()
                                     .Where(where)
                                     .ToList();

            foreach (var entity in entities) {
                var idProperty = entity.GetType()
                                       .GetProperties()
                                       .Where(a => a.Name == "Id")
                                       .FirstOrDefault();

                if (idProperty == null)
                    throw new Exception("Unable to find `Id` property.");

                var idValue = Convert.ToInt32(idProperty.GetValue(entity));
                Repository.Delete(idValue);
            }
            Repository.Save();
        }

        public virtual T Get(Guid id) {
            return Repository.Find(id);
        }

        public virtual T Get(int id) {
            return Repository.Find(id);
        }

        public virtual T Get(Expression<Func<T, bool>> where) {
            return Repository.All().Where(where).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAllBy(Expression<Func<T, bool>> where) {
            return Repository.All().Where(where).ToList();
        }

        public virtual IEnumerable<T> GetAll() {
            return Repository.All();
        }

        public string GeneratePassword() {
            return new CryptographyHelper().CreateHash("patches214");
        }

		public virtual bool Check(int id) {
            var entity = Repository.Find(id);
            if (entity != null)
                return true;
            else
                return false;
        }

        public virtual bool Check(Guid id) {
            var entity = Repository.Find(id);
            if (entity != null)
                return true;
            else
                return false;
        }

        public virtual bool Check(Expression<Func<T, bool>> where) {
            return Repository.All().Any(where);
        }

        #endregion

        #region Settings
        SettingRepository _SettingRepository;
        private SettingRepository RepositorySetting {
            get {
                if (_SettingRepository == null) 
                    _SettingRepository = new SettingRepository();

                return _SettingRepository;
            }
        }

        protected TValue DbSettings<TValue>(string settingName) {
            var setting = RepositorySetting.All().Where(a => a.Name == settingName).FirstOrDefault();
            if (setting == null)
                throw new Exception("Unable to find setting");

            return (TValue)Convert.ChangeType(setting.Value, typeof(TValue));
        }

        protected string DbSettings(string settingName) {
            var setting = RepositorySetting.All().Where(a => a.Name == settingName).FirstOrDefault();
            if (setting == null)
                throw new Exception("Unable to find setting");

            return setting.Value;
        }

        protected void UpdateSetting(string settingName, string value) {
            var setting = RepositorySetting.All().Where(a => a.Name == settingName).FirstOrDefault();
            if (setting == null)
                throw new Exception("Unable to find setting");

            setting.Value = value;
            setting.UpdatedAt = DateTime.Now;

            RepositorySetting.Edit(setting);
            RepositorySetting.Save();
        }

        protected void UpdateSetting(string settingName, string value, bool createIfNonExistent) {
            var setting = RepositorySetting.All().Where(a => a.Name == settingName).FirstOrDefault();
            if (setting == null) {
                if (createIfNonExistent)
                    RepositorySetting.Add(new Domain.Models.Setting {
                                                Name        = settingName,
                                                Value       = value,
                                                CreatedAt   = DateTime.Now
                                            });
                else
                    throw new Exception("Unable to find setting");
            } else {
                setting.Value = value;
                setting.UpdatedAt = DateTime.Now;

                RepositorySetting.Edit(setting);
            }
            RepositorySetting.Save();
        }

        protected TValue ConfigSettings<TValue>(string settingName) {
            return (TValue)Convert.ChangeType(ConfigurationManager.AppSettings[settingName], typeof(TValue));
        }
        #endregion

    }
}