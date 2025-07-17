using DAL.Contracts;
using DAL.Models;
using Domines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DAL.Exceptions;
using System.Linq.Expressions;
namespace DAL.Repositorys
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseTable
    {
        private readonly ShippingContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericRepository<T>> _logger;
        public GenericRepository(ShippingContext context, ILogger<GenericRepository<T>>log)
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
                return _dbSet.Where(a=>a.CurrentState>0).AsNoTracking().ToList();  
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
                return _dbSet.Where(a => a.Id == id).AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }
        // إضافة كائن جديد
        public bool Add(T entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                _dbSet.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }
        public bool Add(T entity, out Guid id)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                _dbSet.Add(entity);
                _context.SaveChanges();
                id = entity.Id;
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }

        // تحديث كائن موجود
        public bool Update(T entity)
        {
            try
            {

                var dbData = GetById(entity.Id);
                //_context.Entry(dbData).State = EntityState.Detached; // فصل الكيان القديم

                entity.CreatedDate = dbData.CreatedDate;
                entity.CreatedBy = dbData.CreatedBy;
                entity.UpdatedDate = DateTime.Now;
                entity.CurrentState = 1;
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }
        // حذف كائن (يتم حذف الكائن من خلال تغيير حالته بدلًا من حذفه فعليًا)
        public bool Delete(T entity)
        {
            try
            {
                // الحذف المنطقي بتغيير حالة الكائن إلى 0 (غير نشط)
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }

            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }
        // تغيير حالة الكائن (على سبيل المثال تفعيل أو إلغاء تفعيل)
        public bool ChangeStatus(Guid id,Guid UserId ,int status = 1)
        {
            try
            {
                var entity = GetById(id);
                if (entity != null)
                {
                    entity.CurrentState = status;
                    entity.UpdatedBy = UserId;
                    entity.UpdatedDate = DateTime.Now;
                    _context.Entry(entity).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                return false;  // في حال لم يتم العثور على الكائن
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

        public async Task< List<T>> GetList(Expression<Func<T, bool>> filter)
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





