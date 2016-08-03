using NEventStore;
using NEventStore.Persistence.Sql.SqlDialects;
using Ninject;

namespace DDD.SimpleExample.WriteSide
{
    public static class EventStoreConfig
    {
        public static IStoreEvents Create(StandardKernel container)
        {
            var store = Wireup.Init()
                .LogToOutputWindow()
                .UsingInMemoryPersistence()
                .UsingSqlPersistence("LocalDb")
                .WithDialect(new MsSqlDialect())
                .EnlistInAmbientTransaction()
                .InitializeStorageEngine()
                .UsingJsonSerialization()
                .UsingSynchronousDispatchScheduler()
                .DispatchTo(new InternalDispatcher(new EventPublisher(container)))
                .Build();
            return store;
        }
    }
}