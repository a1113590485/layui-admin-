using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.Entity.Repository
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity:class
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// 获取所有（效率版，仅查询）
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAllAsNoTracking();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns>IQueryable泛型集合</returns>
        IQueryable<TEntity> GetItemsByPage<TKey>(int pageSize, int pageIndex, Func<TEntity,TKey> orderbyFunc);
        /// <summary>
        /// 获取第一个或默认(按条件)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Func<TEntity, bool> func);
        /// <summary>
        /// 获取第一个或默认(按Id)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(TPrimaryKey key);
        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Get(TPrimaryKey key);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);
        /// <summary>
        /// 添加（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(TEntity entity);
        /// <summary>
        /// 添加并获取Id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TPrimaryKey InsertAndGetId(TEntity entity);
        /// <summary>
        /// 添加并获取Id（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
        /// <summary>
        /// 修改（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// 删除异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(Expression<Func<TEntity, bool>> func);
        /// <summary>
        /// 批量异步删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<TEntity, bool>> func);
    }
}
