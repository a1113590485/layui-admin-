using Bootstrap.Entity.Repository;
using Bootstrap.Entity.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.Entity.Repository
{
    public class BaseRepository<TEntity, TPrimaryKey> : SqlRepository<TEntity, TPrimaryKey> where TEntity : EntityDto<TPrimaryKey>
    {
        public BaseRepository()
            : base()
        {
            base.db = new EntityDBContext();
        }

        public BaseRepository(DbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
