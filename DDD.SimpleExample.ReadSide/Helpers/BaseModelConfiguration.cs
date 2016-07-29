using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using DDD.SimpleExample.ReadSide.Interfaces;

namespace DDD.SimpleExample.ReadSide.Helpers
{
    [InheritedExport(typeof(IModelConfiguration))]
    internal class BaseModelConfiguration<TEntity> : EntityTypeConfiguration<TEntity>, IModelConfiguration
        where TEntity : BaseModel
    {
        protected BaseModelConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Identity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Identity") {IsUnique = true}));
            Property(p => p.AggregateId)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("IX_AggregateId") {IsUnique = true}));
            Property(p => p.RowVersion)
                .IsConcurrencyToken()
                .IsRowVersion();
        }

        public void AddConfiguration(DbModelBuilder registrar)
        {
            registrar.Configurations.Add(this);
        }

        public void AddConfiguration(ConfigurationRegistrar registrar)
        {
            registrar.Add(this);
        }
    }
}