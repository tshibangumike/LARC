using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace larc.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(object id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        // This method was not in the videos, but I thought it would be useful to add.
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Attach(TEntity entity);

        int ExecuteSql(string storedProcedureName,
            List<SqlParameter> listSqlParameter);
    }
}