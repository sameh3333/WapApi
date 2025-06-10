using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DAL.Exceptions;
using System.Linq.Expressions;
namespace DAL.Repositorys
{
    public class ViewRepository<T> : IViewRepository<T> where T : class
    {
        private readonly ShippingContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<ViewRepository<T>> _logger;
        public ViewRepository(ShippingContext context, ILogger<ViewRepository<T>>log)
        {
            _context = context;
            _dbSet = context.Set<T>();  // الحصول على DbSet للكائن T
            _logger = log;
        }
        // جلب جميع الكائنات من الجدول
        public List<T> GetAll()
        {
            try
            {
                return _dbSet.AsNoTracking().ToList();  
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }
        // الحصول على كائن بواسطة الـ Id
        public T GetById(Guid id)
        {
            try
            {
                return _dbSet.AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }




        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            try
            {
                return _dbSet.Where(filter).AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter)
        {
            try
            {
                return _dbSet.Where(filter).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }



    }


}





