using Bootstrap.Entity.Repository;
using Bootstrap.Entity.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bootstrap.Entity.Repository
{
    public abstract class SqlRepository<TEntity,TPrimaryKey>: IRepository<TEntity, TPrimaryKey> where TEntity : EntityDto<TPrimaryKey>
    {
        public DbContext db;
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected SqlRepository()
        {

        }

        protected SqlRepository(DbContext dbContext)
        {
            db = dbContext;
        }

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return db.Set<TEntity>();
            }
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }
        /// <summary>
        /// 获取所有（效率版，仅查询）
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAllAsNoTracking()
        {
            return DbSet.AsNoTracking();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns>IQueryable泛型集合</returns>
        public IQueryable<TEntity> GetItemsByPage<TKey>(int pageSize, int pageIndex, Func<TEntity,TKey> orderbyFunc)
        {
            var temp = DbSet.OrderBy(orderbyFunc)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize);
            return temp.AsQueryable();
        }
        /// <summary>
        /// 获取第一个或默认(按条件)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Func<TEntity,bool> func)
        {
            return DbSet.FirstOrDefault(func);
        }
        /// <summary>
        /// 获取第一个或默认(按Id)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(TPrimaryKey key)
        {
            return DbSet.Where(o => o.Id.ToString() == key.ToString()).FirstOrDefault();
        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Get(TPrimaryKey key)
        {
            var data = DbSet.FirstOrDefault(o => o.Id.ToString() == key.ToString());
            if (data == null) throw new Exception(string.Format("Id为{0}的数据不存在", key));
            return data;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            //添加事务回滚
            using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DbSet.Add(entity);
                db.SaveChanges();
                Ts.Complete();
            }
        }

        /// <summary>
        /// 添加（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DbSet.Add(entity);
                await db.SaveChangesAsync();
                Ts.Complete();
            }
        }
        /// <summary>
        /// 添加并获取Id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DbSet.Add(entity);
                db.SaveChanges();
                Ts.Complete();
                return entity.Id;
            }
        }
        /// <summary>
        /// 添加并获取Id（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DbSet.Add(entity);
                await db.SaveChangesAsync();
                Ts.Complete();
            }
            return entity.Id;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var query = db.Entry(entity);
                if (query.State == EntityState.Detached)
                {
                    query.State = EntityState.Modified;
                }

                db.SaveChanges();

                query.State = EntityState.Detached;
                Ts.Complete();
            }
        }
        /// <summary>
        /// 修改（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var query = db.Entry(entity);
                if (query.State == EntityState.Detached)
                {
                    query.State = EntityState.Modified;
                }

                await db.SaveChangesAsync();

                query.State = EntityState.Detached;
                Ts.Complete();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DbSet.Remove(entity);
                db.SaveChanges();
                Ts.Complete();
            }
        }
        /// <summary>
        /// 删除异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity)
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DbSet.Remove(entity);
                await db.SaveChangesAsync();
                Ts.Complete();
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Delete(Expression<Func<TEntity, bool>> func)
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var EntityModel = db.Set<TEntity>().Where(func);
                if (EntityModel.Count() > 0)
                {
                    db.Set<TEntity>().RemoveRange(EntityModel);
                    db.SaveChanges();
                }
                Ts.Complete();
            }
        }
        /// <summary>
        /// 批量异步删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> func)
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var EntityModel = db.Set<TEntity>().Where(func);
                if (EntityModel.Count() > 0)
                {
                    db.Set<TEntity>().RemoveRange(EntityModel);
                    await db.SaveChangesAsync();
                }
                Ts.Complete();
            }

        }
    }
}
