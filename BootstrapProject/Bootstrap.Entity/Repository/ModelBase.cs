using Bootstrap.Entity.Models;
using Bootstrap.Entity.Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.Entity.Repository
{
    public abstract class ModelBase
    {
        /// <summary>
        /// 用户表
        /// </summary>
        public BaseRepository<User, int> UserRepository = new BaseRepository<User, int>();
        /// <summary>
        /// 角色表
        /// </summary>
        public BaseRepository<Role, int> RoleRepository = new BaseRepository<Role, int>();
        /// <summary>
        /// 权限表
        /// </summary>
        public BaseRepository<Permission, int> PermissionRepository = new BaseRepository<Permission, int>();
        /// <summary>
        /// 用户-角色关联表
        /// </summary>
        public BaseRepository<UserRoleRelation, int> UserRoleRelationRepository = new BaseRepository<UserRoleRelation, int>();
        /// <summary>
        /// 角色-权限关联表
        /// </summary>
        public BaseRepository<RolePermissionRelation, int> RolePermissionRelationRepository = new BaseRepository<RolePermissionRelation, int>();
        /// <summary>
        /// 导航菜单表
        /// </summary>
        public BaseRepository<NavigationMenu, int> NavigationMenuRepository = new BaseRepository<NavigationMenu, int>();
        /// <summary>
        /// 用户-权限关联表
        /// </summary>
        public BaseRepository<UserPermissionRelation, int> UserPermissionRelationRepository = new BaseRepository<UserPermissionRelation, int>();
    }
}
