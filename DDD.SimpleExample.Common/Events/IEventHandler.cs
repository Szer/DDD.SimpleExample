using System.Threading.Tasks;

namespace DDD.SimpleExample.Common.Events
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent e);
    }
}