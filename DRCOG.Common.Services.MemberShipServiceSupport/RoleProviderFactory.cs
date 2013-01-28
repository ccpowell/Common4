using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using DRCOG.Common.Services.MemberShipServiceSupport.Domain;
using DRCOG.Common.Services.MemberShipServiceSupport.Interfaces;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    public static class RoleProviderFactory
    {
        /// <summary>
        /// Names of public role providers. Excludes DrcogRoleProvider.
        /// </summary>
        public static string[] PublicProviderNames = 
        {
            "TripsRoleProvider",
            "ContactRoleProvider",
            "AgingRoleProvider",
            "BiketoWorkRoleProvider"
        };

        private static RoleProvider PROVIDER_DRCOG = Roles.Providers["DrcogRoleProvider"];
        private static RoleProvider PROVIDER_TRIPS = Roles.Providers["TripsRoleProvider"];
        private static RoleProvider PROVIDER_Contact = Roles.Providers["ContactRoleProvider"];
        private static RoleProvider PROVIDER_Aging = Roles.Providers["AgingRoleProvider"];
        private static RoleProvider PROVIDER_BiketoWork = Roles.Providers["BiketoWorkRoleProvider"];

        public static RoleProvider GetProvider(RoleProviderType provider)
        {
            switch (provider)
            {
                case RoleProviderType.DRCOG:
                    return PROVIDER_DRCOG;
                case RoleProviderType.TRIPS:
                    return PROVIDER_TRIPS;
                case RoleProviderType.Contact:
                    return PROVIDER_Contact;
                case RoleProviderType.Aging_Ombudsman:
                    return PROVIDER_Aging;
                case RoleProviderType.BiketoWork:
                    return PROVIDER_BiketoWork;
                default:
                    return null;
            }
        }

        public static RoleProvider GetProvider(string provider)
        {
            return Roles.Providers[provider];
        }

        public abstract class Provider
        {
            private readonly String name;
            private readonly int value;
            private readonly String enumName;


            protected Provider(int value, String name)
            {
                this.value = value;
                this.name = name;
            }

            protected Provider(String name, String enumName)
            {
                this.name = name;
                this.enumName = enumName;
            }

            public override string ToString()
            {
                return this.name;
            }
        }

        public sealed class RoleProviderAttr : Provider
        {
            private RoleProviderAttr(int value, String name)
                : base(value, name) { }
            private RoleProviderAttr(String name, String enumName)
                : base(name, enumName) { instance[enumName] = this; }

            private static readonly Dictionary<string, RoleProviderAttr> instance = new Dictionary<string, RoleProviderAttr>();

            public static readonly RoleProviderAttr CONTACT = new RoleProviderAttr("ContactRoleProvider", RoleProviderType.Contact.ToString());
            public static readonly RoleProviderAttr DRCOG = new RoleProviderAttr("DrcogRoleProvider", RoleProviderType.DRCOG.ToString());
            public static readonly RoleProviderAttr TRIPS = new RoleProviderAttr("TripsRoleProvider", RoleProviderType.TRIPS.ToString());
            public static readonly RoleProviderAttr AGING = new RoleProviderAttr("AgingRoleProvider", RoleProviderType.Aging_Ombudsman.ToString());
            public static readonly RoleProviderAttr BIKETOWORK = new RoleProviderAttr("BiketoWorkProvider", RoleProviderType.BiketoWork.ToString());

            public static explicit operator RoleProviderAttr(string str)
            {
                RoleProviderAttr result;
                if (instance.TryGetValue(str, out result))
                    return result;
                else
                    throw new InvalidCastException();
            }
        }

    }
}
