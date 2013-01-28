using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.Services.MemberShipServiceSupport;
using DRCOG.Common.Domain;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    public abstract class ApplicationStateBase : NotifiableEntity<Guid>
    {
        protected DateTime LastProfileRefresh { get; set; }
        protected DateTime LastRolesRefresh { get; set; }

        public ApplicationStateBase()
        {
            LastProfileRefresh = DateTime.UtcNow.AddMinutes(-15);
        }

        public bool CheckRefreshProfile
        {
            get
            {
                if (DateTime.UtcNow >= LastProfileRefresh.AddMinutes(15))
                {
                    return true;
                }
                return false;
            }
        }

        public bool CheckRefreshRoles
        {
            get
            {
                if (DateTime.UtcNow >= LastRolesRefresh.AddMinutes(1))
                {
                    return true;
                }
                return false;
            }
        }

        public void Refresh()
        {
            LoadRoles();
        }

        public abstract void LoadProfile(ValidateUserResultType validateResultType, string userName);

        public abstract void LoadRoles();
    }
}
