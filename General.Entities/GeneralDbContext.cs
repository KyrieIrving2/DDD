using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using General.Entities;

namespace General.Entities
{
    public class GeneralDbContext: DbContext
    {
        public GeneralDbContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<User.User> oUsers { get; set; }
        public DbSet<Role.Role> oRoles { get; set; }



        public DbSet<Category.Category> Categories { get; set; }
        public DbSet<SysPermission.SysPermission> SysPermissions { get; set; }
        public DbSet<SysRole.SysRole> Roles { get; set; }
        public DbSet<SysUser.SysUser>  Users { get; set; }
        public DbSet<SysUserLoginLog.SysUserLoginLog>  SysUserLoginLogs { get; set; }
        public DbSet<SysUserRole.SysUserRole> SysUserRoles { get; set; }
        public DbSet<SysUserToken.SysUserToken> SysUserTokens { get; set; }

    }
}
