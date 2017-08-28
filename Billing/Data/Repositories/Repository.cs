using System.Collections.Generic;
using System.Linq;
using Billing.Data.Contracts;
using Billing.Entities.Models;

namespace Billing.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Guid
    {
        /// Es Como una BD. Todos los objetos se encuentran acá.
        /// Otra opcion es re-escribir estos 5 metodos, y crear una lista para cada repositorio
        protected static readonly List<Guid> internalList = new List<Guid>();
        protected abstract void Seed();

        public T Create(T entity)
        {
            internalList.Add((T)entity.Clone());
            return entity;
        }

        public void Create(IEnumerable<T> entities)
        {
            internalList.AddRange(entities.ToList().Select(x => (T)x.Clone()));
        }

        public void CreateOrUpdate(T entity)
        {
            if (this.Read(entity) != null)
            {
                this.Update(entity);
            }
            else
            {
                this.Create(entity);
            }
        }

        public void Delete(int entityId)
        {
            internalList.RemoveAll(x => x.Id == entityId);
        }

        public IEnumerable<T> List()
        {
            return internalList.FindAll(x => x.GetType() == typeof(T)).ConvertAll(x => (T)x);
        }

        public T Read(int entityId)
        {
            return (T)internalList.Find(x => x.Id == entityId)?.Clone();
        }

        public T Read(T entity)
        {
            return (T)internalList.Find(x => x.Id == entity.Id)?.Clone();
        }

        public void Update(T entity)
        {
            internalList.RemoveAll(x => x.Id == entity.Id);
            internalList.Add((T)entity.Clone());
        }
    }
}