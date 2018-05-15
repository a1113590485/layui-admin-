using Bootstrap.Entity.Models;
using Bootstrap.Entity.Models.System;
using Bootstrap.Entity.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.Entity.Repository
{
    public partial class EntityDBContext: DbContext
    {
        static EntityDBContext()
        {
            Database.SetInitializer<EntityDBContext>(null);
        }
        public EntityDBContext()
            : base("name=TestDBEntities")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
        public IDbSet<Permission> PermissionSet { get; set; }
        public IDbSet<Role> RoleSet { get; set; }
        public IDbSet<RolePermissionRelation> RolePermissionRelationSet { get; set; }
        public IDbSet<User> UserSet { get; set; }
        public IDbSet<UserRoleRelation> UserRoleRelationSet { get; set; }
        public IDbSet<NavigationMenu> NavigationMenuSet { get; set; }
        public IDbSet<UserPermissionRelation> UserPermissionRelationSet { get; set; }
    }
}
