using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.Interfaces.QueueSupport;
using DRCOG.Common.Services.QueueSupport.QueueCommand;
using System.Transactions;

namespace DRCOG.Common.Services.QueueSupport
{
    public class QueueReceiver<T> : IQueueReceiver
    {
        private Queue<QueueCommand<T>> _queue;
        private Stack<QueueCommand<T>> _undoStack;

        private readonly Guid _sessionId;

        public QueueReceiver() : this(Guid.NewGuid()) { }

        public QueueReceiver(Guid sessionId)
        {
            _queue = new Queue<QueueCommand<T>>();
            _undoStack = new Stack<QueueCommand<T>>();
            _sessionId = sessionId;
        }

        public int Count
        {
            get { return _queue.Count(); }
        }

        public void Enqueue(QueueCommand<T> value)
        {
            _queue.Enqueue(value);
        }

        public void Process()
        {
            QueueCommand<T> queueCommand;
            int retry = 0;
            const int maxRetry = 3;
            using (TransactionScope scope = new TransactionScope())
            {
                do
                {
                    queueCommand = _queue.Peek();
                    if (queueCommand.Commit(_sessionId))
                    {
                        // do action
                        _undoStack.Push(_queue.Dequeue());
                        retry = 0;
                    }
                    else retry++;

                } while (_queue.Count > 0 && retry < maxRetry);

                if (retry == 0)
                {
                    scope.Complete();
                }
                else
                {
                    foreach (QueueCommand<T> item in _undoStack)
                    {
                        Enqueue(_undoStack.Pop());
                    }
                    throw new TransactionAbortedException();
                }
            }
        }
    }
}
