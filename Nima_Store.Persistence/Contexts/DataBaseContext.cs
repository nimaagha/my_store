﻿using Microsoft.EntityFrameworkCore;
using Nima_Store.Application.Interfaces.Contexts;
using Nima_Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nima_Store.Persistence.Contexts
{
    public class DataBaseContext:DbContext ,IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) :base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
    }
}