

using Domines;
using System.Linq.Expressions;

namespace DAL.Contracts
{
    public interface IViewRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(Guid id);  // أو Domines.Guid إذا كنت تستخدمه
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        List<T> GetList(Expression<Func<T, bool>> filter);
    }


}
