using GM.QueueService.QueueDTO;
using GeoMed.Main.DTO.Patients;
using System;
using System.Threading;
using System.Threading.Tasks;
using MainDomain.IRepositories;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.Logging;

namespace QueueService
{
    public class MessageQueueService : IMessageQueueService<QueueMessage>
        // , IConsume<QueueMessage>
    {
        //public void Consume(QueueMessage message, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}
      //  private ILogger LogService { get; }
        private IMainDomain MainDomain { get; }

        public MessageQueueService(IMainDomain mainDomain
            //, ILoggerFactory logger
            )
        {
            MainDomain = mainDomain;
            //LogService = logger.CreateLogger("QueueMessages"); ;
        }


        [AutoSubscriberConsumer(SubscriptionId = "ClientMessageService")]
        public void Consume(QueueMessage message, CancellationToken cancellationToken = default)
        {
            Type messageType = message.Type;

           // LogService.LogDebug("add pateint");
            if (messageType == typeof(GetPatientDto))
            {
                var data = message.GetData<GetPatientDto>();
                MainDomain.ActionPatiants(new MainDomain.MainDTO.PatientDto()
                {
                    AreaId = 9 ,
                    BirthDay = data.LastInComeDate,
                    FirsthName = data.PatientName,
                    LastName = data.PatientName
                }) ;
            }

        }
    }
}
