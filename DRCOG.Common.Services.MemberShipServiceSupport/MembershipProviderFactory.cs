using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using DRCOG.Common.Services.MemberShipServiceSupport.Domain;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    public static class MembershipProviderFactory
    {
        private static MembershipProvider MEMBERSHIP_PROVIDER_DRCOG = Membership.Providers["DrcogMembershipProvider"];
        //private static MembershipProvider MEMBERSHIP_PROVIDER_TRIPS = Membership.Providers["TripsMembershipProvider"];

        public static MembershipProvider GetProvider(MembershipProviderType provider)
        {
            switch (provider)
            {
                case MembershipProviderType.DRCOG:
                    return MEMBERSHIP_PROVIDER_DRCOG;
                //case Enums.MembershipProvider.TRIPS:
                //    return MEMBERSHIP_PROVIDER_TRIPS;
                default:
                    return null;
            }
        }

    }
}
