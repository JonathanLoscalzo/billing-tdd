using System.Collections.Generic;

namespace Billing.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        T Create(T entity);

        void Create(IEnumerable<T> entities);

        T Read(int entityId);

        void Update(T entity);

        void Delete(int entityId);

        IEnumerable<T> List();
    }
}