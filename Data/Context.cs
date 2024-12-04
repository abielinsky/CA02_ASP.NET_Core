using CA02_ASP.NET_Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CA02_ASP.NET_Core.Data
{

    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<RentalEntity> Rentals { get; set; }
        public DbSet<UsersEntity> Users { get; set; }
        
    }


}
