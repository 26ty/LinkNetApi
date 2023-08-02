using System;
using Microsoft.EntityFrameworkCore;
using LinkNetApi.Models;

namespace LinkNetApi.Models
{
    //public class DbContext
    //{
    //	public DbContext()
    //	{
    //	}
    //}

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; } = default!;

        //public DbSet<YourEntity> YourEntities { get; set; }
    }
}

