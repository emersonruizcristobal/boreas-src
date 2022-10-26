using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RefactorThis.GraphDiff;
using System.Collections.Generic;

namespace ERC.Framework.Repository {
    public abstract class BaseRepository<C, T> : IBaseRepository<T>, IDisposable
        where C : DbContext, new()
        where T : class, new() {
        private C _entities = Activator.CreateInstance<C>();
        public C Context {
            get {
                return this._entities;
            }set{
                this._entities = value;
            }
        }
        public virtual IQueryable<T> All() {
            return this._entities.Set<T>();
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties) {
            IQueryable<T> queryable = this._entities.Set<T>();
            for (int i = 0; i < includeProperties.Length; i++) {
                Expression<Func<T, object>> expression = includeProperties[i];
                queryable = QueryableExtensions.Include<T, object>(queryable, expression);
            }
            return queryable;
        }
        public virtual T Find(int id) {
            return this._entities.Set<T>().Find(new object[] {
				id
			});
        }
        public virtual T Find(Guid id) {
            return this._entities.Set<T>().Find(new object[] {
				id
			});
        }
        public virtual void Add(T entity) {
            this._entities.Set<T>().Add(entity);
        }

        public virtual void Add(IEnumerable<T> entities) {
            foreach(var entity in entities)
                this._entities.Set<T>().Add(entity);
        }

        public virtual void Edit(T entity) {
            if (this._entities.Set<T>().Local.Any(e => e == entity)) {
                this._entities.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            } else {
                this._entities.UpdateGraph<T>(entity);
            }
        }

        public virtual void Edit(IEnumerable<T> entities) {
            foreach (var entity in entities) {
                if (this._entities.Set<T>().Local.Any(e => e == entity)) {
                    this._entities.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
                } else {
                    this._entities.UpdateGraph<T>(entity);
                }
            }
        }

        public virtual void Edit(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping) {
            this._entities.UpdateGraph<T>(entity, mapping);
        }

        public virtual void Edit(IEnumerable<T> entities, Expression<Func<IUpdateConfiguration<T>, object>> mapping) {
            foreach(var entity in entities)
                this._entities.UpdateGraph<T>(entity, mapping);
        }

        public virtual void Delete(int id) {
            T t = this._entities.Set<T>().Find(new object[] {
				id
			});
            this._entities.Set<T>().Remove(t);
        }
        public virtual void Delete(Guid id) {
            T t = this._entities.Set<T>().Find(new object[] {
				id
			});
            this._entities.Set<T>().Remove(t);
        }
        public virtual void Save() {
            this._entities.SaveChanges();
        }

        public virtual async System.Threading.Tasks.Task SaveAsync() {
            await this._entities.SaveChangesAsync();
        }

        public virtual void Dispose() {
            this._entities.Dispose();
        }
    }

    public interface IBaseRepository<T> : IDisposable where T : class {
        IQueryable<T> All();

        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        T Find(int id);

        T Find(Guid id);

        void Add(T entity);

        void Add(IEnumerable<T> entity);

        void Edit(T entity);

        void Edit(IEnumerable<T> entity);

        void Delete(int id);

        void Delete(Guid id);

        void Save();
    }

    public class Paginate<T> {
        private string keywords;
        public IQueryable<T> Items {
            get;
            set;
        }
        public string Keywords {
            get {
                if (!string.IsNullOrEmpty(this.keywords)) {
                    return this.keywords;
                }
                return string.Empty;
            }
            set {
                this.keywords = (string.IsNullOrEmpty(value) ? string.Empty : value);
            }
        }
        public int CurrentPageIndex {
            get;
            set;
        }
        public int PageSize {
            get;
            set;
        }
        public int TotalItemCount {
            get;
            set;
        }
        public int TotalPageCount {
            get;
            private set;
        }
        public int StartRecordIndex {
            get;
            private set;
        }
        public int EndRecordIndex {
            get;
            private set;
        }
        public Paginate(IQueryable<T> allItems, int pageIndex, int pageSize, string keywords) {
            if (pageIndex < 1) {
                pageIndex = 1;
            }
            int count = (pageIndex - 1) * pageSize;
            int num = allItems.Count<T>();
            this.Items = allItems.Skip(count).Take(pageSize);
            this.TotalItemCount = num;
            this.TotalPageCount = (int)Math.Ceiling((double)num / (double)pageSize);
            this.CurrentPageIndex = pageIndex;
            this.PageSize = pageSize;
            this.StartRecordIndex = (pageIndex - 1) * pageSize + 1;
            this.EndRecordIndex = ((this.TotalItemCount > pageIndex * pageSize) ? (pageIndex * pageSize) : num);
            this.Keywords = keywords;
        }
    }

    public static class PageLinqExtensions {
        public static Paginate<T> Paginate<T>(this IQueryable<T> allItems, int pageIndex, int pageSize, string keywords) {
            return new Paginate<T>(allItems, pageIndex, pageSize, keywords);
        }
    }
}
