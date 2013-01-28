using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DRCOG.Common.Services.QueueSupport.QueueCommand
{
    [DataContract]
    public abstract class QueueCommand<T>
    {
        //private T _model;
        protected QueueReceiver<T> _receiver;
        public readonly Guid _changeId;

        //public T model
        //{
        //    get { return _model; }
        //    set
        //    {
        //        _model = value;

        //    }
        //}

        public QueueCommand()
        {
            _changeId = Guid.NewGuid();
        }

        public QueueCommand(QueueReceiver<T> receiver)
            : this()
        {
            _receiver = receiver;
            //model = (T)Activator.CreateInstance(typeof(T));
        }

        //public QueueCommand(QueueReceiver<T> receiver, T model)
        //    : this()
        //{
        //    _receiver = receiver;
        //    this.model = model;
        //}

        public abstract bool Commit(Guid sId);
    }
}
