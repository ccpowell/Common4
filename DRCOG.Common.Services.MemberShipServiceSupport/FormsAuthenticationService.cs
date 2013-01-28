﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using DRCOG.Common.Service.MemberShipServiceSupport.Interfaces;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);


        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
