using System.Threading.Tasks;

namespace DDD.SimpleExample.Common.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync(dynamic e);
    }
}