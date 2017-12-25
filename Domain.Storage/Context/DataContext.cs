using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Storage.Context
{
    public class DataContext:DbContext
    {
        public DataContext() : base(DomainConstants.Connectionstring)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext,Migrations.Configuration>(DomainConstants.Connectionstring));

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
