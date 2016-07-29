using System;
using DDD.SimpleExample.Common.Events;
using NEventStore;
using NEventStore.Dispatcher;

namespace DDD.SimpleExample.WriteSide
{
    public class InternalDispatcher : IDispatchCommits
    {
        private readonly IEventPublisher _publisher;

        public InternalDispatcher(IEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Dispatch(ICommit commit)
        {
            foreach (var e in commit.Events)
            {
                //var messageType = e.Body.GetType();
                _publisher.PublishAsync(e.Body);
            }
        }
    }
}