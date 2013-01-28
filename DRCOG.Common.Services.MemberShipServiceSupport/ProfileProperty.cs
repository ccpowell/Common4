using System.Runtime.Serialization;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    [DataContract]
    public class ProfileProperty
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public ProfilePropertyType Type { get; set; }

        public ProfileProperty()
        {
        }

        public ProfileProperty(int ID, string name)
        {
            this.ID = ID;
            this.Name = name;
        }
    }
}
