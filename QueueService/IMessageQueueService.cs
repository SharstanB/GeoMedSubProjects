using EasyNetQ.AutoSubscribe;
using GM.QueueService.QueueDTO;

namespace QueueService
{
    public interface IMessageQueueService<T>
        :
         IConsume<QueueMessage> 
        // , IConsume<QueueMessage>
    {
    }
}
