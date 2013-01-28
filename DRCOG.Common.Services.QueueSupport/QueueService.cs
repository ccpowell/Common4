using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.Interfaces.QueueSupport;
using DRCOG.Common.CustomEvents;
using DRCOG.Common.DesignByContract;
using DRCOG.Common.Domain;

namespace DRCOG.Common.Services.QueueSupport
{
    public class QueueService
    {
        private readonly Guid _sessionId;

        public Queue<IQueueReceiver> queue { get; protected set; }

        private IQueueReceiver Receiver;

        public QueueService()
        {
            queue = new Queue<IQueueReceiver>();
            _sessionId = Guid.NewGuid();
        }

        public QueueReceiver<T> InitReceiver<T>() where T : INotifyPropertyChange
        {
            Receiver = new QueueReceiver<T>(_sessionId);
            return (Receiver as QueueReceiver<T>);
        }

        public void EnqueueReceiver()
        {
            queue.Enqueue(Receiver);
            Receiver = null;
        }

        public void EnqueueReceiver(IQueueReceiver receiver)
        {
            queue.Enqueue(receiver);
            Receiver = null;
        }

        public void ProcessQueue()
        {
            Check.Assert(queue.Count > 0, "There is nothing in the queue to process.");
            foreach (IQueueReceiver r in queue)
            {
                r.Process();
            }
        }
    }
}
