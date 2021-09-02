using GM.QueueService.QueueDTO;
using GeoMed.Main.DTO.Patients;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QueueServiceDomain
{
    public class QueueService : IQueueService
    {
        //public void Consume(QueueMessage message, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        public Task ConsumeAsync(QueueMessage message, CancellationToken cancellationToken = default)
        {
            Type messageType = message.Type;

            if (messageType == typeof(GetPatientDto))
            {
                
            }

            throw new NotImplementedException();
        }
    }
}
