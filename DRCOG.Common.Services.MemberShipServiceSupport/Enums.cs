using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    public enum MembershipProviderType
    {
        DRCOG
        ,
        TRIPS
        ,
        Contact
        ,
        Aging_Ombudsman
        ,
        BiketoWork
    }
    public enum RoleProviderType
    {
        DRCOG
        ,
        TRIPS
        ,
        Contact
        ,
        Aging_Ombudsman
        ,
        BiketoWork
    }

    

    [DataContract]
    public enum ValidateUserResultType
    {
        [EnumMember]
        Domain = 1,

        [EnumMember]
        Membership = 2,

        [EnumMember]
        OpenId = 3,
        
        [EnumMember]
        Error = 4
    }

    [DataContract]
    public enum PasswordResetResultType
    {
        [EnumMember]
        EmailNotFound = 1,

        [EnumMember]
        Successful = 2,

        [EnumMember]
        Error = 3
    }

    [DataContract]
    public enum CustomProfilePropertyValueResultType
    {
        [EnumMember]
        PropertyNotFound = 1,

        [EnumMember]
        PropertyFound = 2,

        [EnumMember]
        Error = 3
    }

    [DataContract]
    public enum ProfilePropertyType
    {
        [EnumMember]
        String = 1,

        [EnumMember]
        Double = 2,

        [EnumMember]
        Int = 3,

        [EnumMember]
        Long = 4,

        [EnumMember]
        DateTime = 5
    }
}
