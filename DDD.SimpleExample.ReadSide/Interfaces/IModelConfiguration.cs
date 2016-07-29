using System.Data.Entity.ModelConfiguration.Configuration;

namespace DDD.SimpleExample.ReadSide.Interfaces
{
    internal interface IModelConfiguration
    {
        void AddConfiguration(ConfigurationRegistrar registrar);
    }
}