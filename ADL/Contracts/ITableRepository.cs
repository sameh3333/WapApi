

using Domines;
using System.Linq.Expressions;

namespace DAL.Contracts
{
    public interface IGenericRepository<T> where T : BaseTable
    {
        List<T> GetAll();
        T GetById(Guid id);  // أو Domines.Guid إذا كنت تستخدمه
        bool Add(T entity); 
        bool Add(T entity, out Guid id );
        bool Update(T entity);
        bool Delete(T entity);
        bool ChangeStatus(Guid id,Guid userId, int status = 1);  // أو Domines.Guid


        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        Task<List<T>> GetList(Expression<Func<T, bool>> filter);



    }


}
