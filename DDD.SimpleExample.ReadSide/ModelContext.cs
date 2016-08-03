using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using DDD.SimpleExample.ReadSide.Helpers;
using DDD.SimpleExample.ReadSide.Interfaces;
using DDD.SimpleExample.ReadSide.Models;

namespace DDD.SimpleExample.ReadSide
{
    public class ModelContext : DbContext, IModelUpdater, IModelReader
    {
        public ModelContext()
            : base("name=LocalDb")
        {
            Database.SetInitializer<ModelContext>(null);
        }

        public virtual DbSet<CustomerModel> Customers { get; set; }
        public virtual DbSet<ProjectModel> Projects { get; set; }
        public virtual DbSet<UserModel> Users { get; set; }

        IQueryable<ProjectModel> IModelReader.Projects => Projects.AsNoTracking();
        IQueryable<CustomerModel> IModelReader.Customers => Customers.AsNoTracking();
        IQueryable<UserModel> IModelReader.Users => Users.AsNoTracking();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var contextConfiguration = new ContextConfiguration();
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(contextConfiguration);

            foreach (var configuration in contextConfiguration.Configurations)
            {
                configuration.AddConfiguration(modelBuilder.Configurations);
            }
        }
    }
}