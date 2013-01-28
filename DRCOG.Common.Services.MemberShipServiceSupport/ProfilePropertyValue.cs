using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using DRCOG.Common.Interfaces;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    [DataContract]
    public class ProfilePropertyValue : IEquatable<ProfilePropertyValue>, IPropertyValue
    {        
        [DataMember]
        public int? ID { get; set; }
        [DataMember]
        public string PropertyName { get; set; }
        [DataMember]
        public string PropertyValue { get; set; }
        [DataMember]
        public int PropertyID { get; set; }
        [DataMember]
        public bool IsUnused { get; set; }
        [DataMember]
        public bool IsTransient { get; set; }
        [DataMember]
        public bool HasChange { get; set; }


        public ProfilePropertyValue()
        {
        }

        public ProfilePropertyValue(int ID, string propertyName, string propertyValue, int propertyID, bool isUnused)
        {
            this.ID = ID;
            this.PropertyName = propertyName;
            this.PropertyValue = propertyValue;
            this.PropertyID = propertyID;
            this.IsUnused = isUnused;
        }

        /*
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        */

        public bool Equals(ProfilePropertyValue other)
        {
            // check whether the compared object is null
            if (Object.ReferenceEquals(other, null))
                return false;

            // check whether compared objects reference the same data
            if (Object.ReferenceEquals(this, other)) return true;

            // check whether the properties are equal
            return this.PropertyName == other.PropertyName
                && this.PropertyID == other.PropertyID
                && this.PropertyValue == other.PropertyValue;
        }
    }

    public class ProfilePropertyValueComparer : IEqualityComparer<ProfilePropertyValue>
    {

        public bool Equals(ProfilePropertyValue x, ProfilePropertyValue y)
        {
            // check whether both the objects reference the same data
            if (Object.ReferenceEquals(x, y)) return true;

            // check whether any of the object is null
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            // check whether the properties are equal
            return x.PropertyID == y.PropertyID && x.PropertyName == y.PropertyName && x.PropertyValue == y.PropertyValue;
        }

        public int GetHashCode(ProfilePropertyValue obj)
        {
            // check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            // get hash code for property name
            int propertyNameHashCode = obj.PropertyName == null ? 0 : obj.PropertyName.GetHashCode();

            // get hash code for property id
            int propertyIdHashCode = obj.PropertyName.GetHashCode();

            // get hash code for property value
            int propertyValueHashCode = obj.PropertyValue.GetHashCode();

            // calculate the hash code for the object
            return propertyIdHashCode ^ propertyIdHashCode ^ propertyValueHashCode;
        }
    }
}
