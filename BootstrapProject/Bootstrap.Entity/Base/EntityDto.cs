using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.Entity.Base
{
    public abstract class EntityDto<TPrimaryKey>
    {
        [Key]
        public TPrimaryKey Id { get; set; }
    }
}
