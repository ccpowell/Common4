using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DRCOG.Common.Domain.Attributes;

namespace DRCOG.Common.Domain
{
    [Serializable]
    public abstract class BaseEntity<IdT> : IEntity<IdT>
    {
        /// <summary>
        /// This particular magic number is often used in GetHashCode computations but is actually 
        /// quite random.  Resharper uses 397 as its number when overrideing GetHashCode, so it 
        /// either started there or has a deeper and more profound history than 42.
        /// 
        /// And yes, I know it's ironic having a constant with the word "random" in its name.
        /// </summary>
        private const int RANDOM_PRIME_NUMBER = 397;

        public override bool Equals(object obj)
        {
            BaseEntity<IdT> compareTo = obj as BaseEntity<IdT>;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (compareTo == null || !GetType().Equals(compareTo.GetTypeUnproxied()))
                return false;

            if (HasSameNonDefaultIdAs(compareTo))
                return true;

            // Since the Ids aren't the same, both of them must be transient to 
            // compare domain signatures; because if one is transient and the 
            // other is a persisted entity, then they cannot be the same object.
            return IsTransient() && compareTo.IsTransient() &&
                HasSameObjectSignatureAs(compareTo);
        }

        /// <summary>
        /// Used to provide the hashcode identifier of an object using the signature 
        /// properties of the object; although it's necessary for NHibernate's use, this can 
        /// also be useful for business logic purposes and has been included in this base 
        /// class, accordingly.  Since it is recommended that GetHashCode change infrequently, 
        /// if at all, in an object's lifetime; it's important that properties are carefully
        /// selected which truly represent the signature of an object.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                IEnumerable<PropertyInfo> signatureProperties = GetSignatureProperties();

                // It's possible for two objects to return the same hash code based on 
                // identically valued properties, even if they're of two different types, 
                // so we include the object's type in the hash calculation
                int hashCode = GetType().GetHashCode();

                foreach (PropertyInfo property in signatureProperties)
                {
                    object value = property.GetValue(this, null);

                    if (value != null)
                        hashCode = (hashCode * RANDOM_PRIME_NUMBER) ^ value.GetHashCode();
                }

                if (signatureProperties.Any())
                    return hashCode;

                // If no properties were flagged as being part of the signature of the object,
                // then simply return the hashcode of the base object as the hashcode.
                return base.GetHashCode();
            }
        }

        /// <summary>
        /// You may override this method to provide your own comparison routine.
        /// </summary>
        protected virtual bool HasSameObjectSignatureAs(BaseEntity<IdT> compareTo)
        {
            IEnumerable<PropertyInfo> signatureProperties = GetSignatureProperties();

            foreach (PropertyInfo property in signatureProperties)
            {
                object valueOfThisObject = property.GetValue(this, null);
                object valueToCompareTo = property.GetValue(compareTo, null);

                if (valueOfThisObject == null && valueToCompareTo == null)
                    continue;

                if ((valueOfThisObject == null ^ valueToCompareTo == null) ||
                    (!valueOfThisObject.Equals(valueToCompareTo)))
                {
                    return false;
                }
            }

            // If we've gotten this far and signature properties were found, then we can
            // assume that everything matched; otherwise, if there were no signature 
            // properties, then simply return the default bahavior of Equals
            return signatureProperties.Any() || base.Equals(compareTo);
        }

        /// <summary>
        /// </summary>
        public virtual IEnumerable<PropertyInfo> GetSignatureProperties()
        {
            IEnumerable<PropertyInfo> properties;

            // Init the signaturePropertiesDictionary here due to reasons described at 
            // http://blogs.msdn.com/jfoscoding/archive/2006/07/18/670497.aspx
            if (signaturePropertiesDictionary == null)
                signaturePropertiesDictionary = new Dictionary<Type, IEnumerable<PropertyInfo>>();

            if (signaturePropertiesDictionary.TryGetValue(GetType(), out properties))
                return properties;

            return (signaturePropertiesDictionary[GetType()] = GetTypeSpecificSignatureProperties());
        }


        /// <summary>
        /// When NHibernate proxies objects, it masks the type of the actual entity object.
        /// This wrapper burrows into the proxied object to get its actual type.
        /// 
        /// Although this assumes NHibernate is being used, it doesn't require any NHibernate
        /// related dependencies and has no bad side effects if NHibernate isn't being used.
        /// 
        /// Related discussion is at http://groups.google.com/group/sharp-architecture/browse_thread/thread/ddd05f9baede023a ...thanks Jay Oliver!
        /// </summary>
        protected virtual Type GetTypeUnproxied()
        {
            return GetType();
        }

        /// <summary>
        /// This static member caches the domain signature properties to avoid looking them up for 
        /// each instance of the same type.
        /// 
        /// A description of the very slick ThreadStatic attribute may be found at 
        /// http://www.dotnetjunkies.com/WebLog/chris.taylor/archive/2005/08/18/132026.aspx
        /// </summary>
        [ThreadStatic]
        private static Dictionary<Type, IEnumerable<PropertyInfo>> signaturePropertiesDictionary;

        #region IEntity Members

        /// <summary>
        /// EntityId may be of type string, int, custom type, etc.
        /// It's virtual to allow NHibernate-backed objects to be lazily loaded.
        /// </summary>
        [IgnoreOnUpdate]
        public virtual IdT EntityId { get; set; }

        #endregion

        #region Entity comparison support

        /// <summary>
        /// Transient objects are not associated with an item already in storage.  For instance,
        /// a Customer is transient if its Id is 0.  It's virtual to allow NHibernate-backed 
        /// objects to be lazily loaded.
        /// </summary>
        public virtual bool IsTransient()
        {
            return EntityId == null || EntityId.Equals(default(IdT));
        }

        /// <summary>
        /// The property getter for SignatureProperties should ONLY compare the properties which make up 
        /// the "domain signature" of the object.
        /// 
        /// If you choose NOT to override this method (which will be the most DRCOG.Common scenario), 
        /// then you should decorate the appropriate property(s) with [DomainSignature] and they 
        /// will be compared automatically.  This is the preferred method of managing the domain
        /// signature of entity objects.
        /// </summary>
        /// <remarks>
        /// This ensures that the entity has at least one property decorated with the 
        /// [DomainSignature] attribute.
        /// </remarks>
        protected virtual IEnumerable<PropertyInfo> GetTypeSpecificSignatureProperties()
        {
            return GetType().GetProperties()
                .Where(propetyInfo => Attribute.IsDefined(propetyInfo, typeof(DomainSignatureAttribute), true));
        }

        public virtual String GetEntityTable()
        {
            return (GetType().GetCustomAttributes(typeof(TableAttribute), true)
                .SingleOrDefault() as TableAttribute)
                .Name;
        }

        /// <summary>
        /// Returns true if self and the provided entity have the same Id values 
        /// and the Ids are not of the default Id value
        /// </summary>
        private bool HasSameNonDefaultIdAs(BaseEntity<IdT> compareTo)
        {
            return !IsTransient() &&
                  !compareTo.IsTransient() &&
                  EntityId.Equals(compareTo.EntityId);
        }

        #endregion
    }
}
