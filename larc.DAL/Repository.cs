using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace larc.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly LarcContext Context;

        public Repository(LarcContext context)
        {
            Context = context;
        }

        public TEntity Get(object id)
        {
            // Here we are working with a DbContext, not PlutoContext. So we don't have DbSets 
            // such as Courses or Authors, and we need to use the generic Set() method to access them.
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            // Note that here I've repeated Context.Set<TEntity>() in every method and this is causing
            // too much noise. I could get a reference to the DbSet returned from this method in the 
            // constructor and store it in a private field like _entities. This way, the implementation
            // of our methods would be cleaner:
            // 
            // _entities.ToList();
            // _entities.Where();
            // _entities.SingleOrDefault();
            // 
            // I didn't change it because I wanted the code to look like the videos. But feel free to change
            // this on your own.
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Attach(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
        }

        public int ExecuteSql(string storedProcedureName, List<SqlParameter> listSqlParameter)
        {
            var context = new LarcContext();
            using (var sqlConnection = new SqlConnection(context.Database.Connection.ConnectionString))
            {
                sqlConnection.Open();
                try
                {
                    var command = new SqlCommand
                    {
                        CommandText = storedProcedureName,
                        Connection = sqlConnection,
                        CommandType = CommandType.StoredProcedure
                    };
                    var resultParameter = command.CreateParameter();
                    resultParameter.ParameterName = "@result";
                    resultParameter.Direction = ParameterDirection.Output;
                    resultParameter.DbType = DbType.Int32;
                    resultParameter.Value = -1;
                    if (listSqlParameter != null)
                    {
                        foreach (var parameter in listSqlParameter)
                        {
                            command.Parameters.Add(parameter);
                        }

                        command.Parameters.Add(resultParameter);
                    }
                    command.ExecuteNonQuery();
                    return (int)resultParameter.Value;
                }
                catch (Exception ex)
                {
                    //Log.Error("ExecutePostSqlStoredProcedure - " + storedProcedureName, ex);
                }
            }
            return -1;
        }
    }
}
