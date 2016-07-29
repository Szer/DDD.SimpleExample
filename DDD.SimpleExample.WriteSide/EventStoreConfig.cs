using NEventStore;
using NEventStore.Persistence.Sql.SqlDialects;
using SimpleInjector;

namespace DDD.SimpleExample.WriteSide
{
    public static class EventStoreConfig
    {
        public static IStoreEvents Create(Container container)
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

            //var client = new PollingClient(store.Advanced);
            //var observer = client.ObserveFrom();

            //var publisher = new EventPublisher(container);
            //var sub = observer.Subscribe(publisher);
            return store;
        }
    }
}
