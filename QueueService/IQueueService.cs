using EasyNetQ.AutoSubscribe;
using GM.QueueService.QueueDTO;

namespace QueueServiceDomain
{
    public interface IQueueService
        : IConsumeAsync<QueueMessage> 
        //, IConsume<QueueMessage>
    {
    }
}
