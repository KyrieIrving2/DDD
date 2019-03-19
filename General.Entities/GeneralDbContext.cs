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

        public DbSet<User.User> Users { get; set; }

        public DbSet<Role.Role> Roles { get; set; }
    }
}
