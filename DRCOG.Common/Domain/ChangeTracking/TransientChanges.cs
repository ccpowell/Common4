using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DRCOG.Common.Util;
using System.Collections.ObjectModel;

namespace DRCOG.Common.Domain
{
    public class TransientChanges<T, IdT> where T : NotifiableEntity<IdT>
    {
        private readonly List<PropertyChange> _changeLog;
        private T _entity;

        public TransientChanges(T entity)
        {
            this._changeLog = new List<PropertyChange>();
            this._entity = entity;

            this.PopulateTransientChanges<T, IdT>(_entity);
        }

        protected void AddChange(PropertyChange change)
        {
            this._changeLog.Add(change);
        }

        public List<PropertyChange> ChangeLog
        {
            get { return this._changeLog; }
        }

        public void PopulateTransientChanges<L, IdL>(L entity) where L : NotifiableEntity<IdL>
        {
            try
            {
                IEnumerable<PropertyInfo> properties = entity.GetSignatureProperties();
                string tableName = String.Empty;
                if (!String.IsNullOrEmpty(tableName = entity.GetEntityTable()))
                {
                    foreach (PropertyInfo info in properties)
                    {
                        Type ptype = info.PropertyType;
                        ptype = Nullable.GetUnderlyingType(ptype) ?? ptype;
                        object value = info.GetValue(entity, null);
                        if (ptype.IsSubclassOfRawGeneric(typeof(NotifiableEntity<>)))
                        {
                            typeof(TransientChanges<L, IdL>).GetMethod("PopulateTransientChanges").MakeGenericMethod(new Type[] { ptype, typeof(IdL) })
                                .Invoke(this, new object[] { value });
                        }
                        else
                        {

                            if (value != null && !value.Equals(info.GetDefault()))
                            {
                                this.AddChange(new PropertyChange(tableName + "." + info.Name, value, null));
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {

            }
        }
    }
}
