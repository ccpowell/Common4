using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DRCOG.Common.Services.MemberShipServiceSupport.Domain
{
    /// <summary>
    /// Model for a user with roles. 
    /// </summary>
    /// <remarks>This is mostly an input model.</remarks>
    public class UserRole
    {
        public Guid PersonGuid { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public RoleProviderType RoleProvider { get; set; }

        public Dictionary<string, bool> Roles { get; set; }
    }

}
