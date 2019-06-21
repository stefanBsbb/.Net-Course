using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        List<T> GetAll();

        T GetByID(int id);

        void Create(T item);

        void Update(T item, Func<T, bool> findByIDPredecate);

        

        void Save(T item);
    }
}
