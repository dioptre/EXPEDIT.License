﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;
using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Core.Common.Models;
using Orchard.Core.XmlRpc;
using Orchard.Core.XmlRpc.Models;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Mvc.Extensions;
using Orchard.Security;
using Orchard.Mvc.Html;
using Orchard.Core.Title.Models;
using Newtonsoft.Json;
using CNX.Shared.Helpers;
using CNX.Shared.Models;
using EXPEDIT.Share.Helpers;
using System.Dynamic;
using ImpromptuInterface;
using NKD.Services;
using EXPEDIT.License.ViewModels;
using NKD.Helpers;

namespace EXPEDIT.License.Services {
    [UsedImplicitly]
    public class XmlRpcHandler : IXmlRpcHandler {
        private readonly IContentManager _contentManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMembershipService _membershipService;
        private readonly RouteCollection _routeCollection;
        private readonly ILicenseService _license;
        private readonly IUsersService _users;

        public XmlRpcHandler(IContentManager contentManager,
            IAuthorizationService authorizationService, 
            IMembershipService membershipService, 
            RouteCollection routeCollection,
            ILicenseService license,
            IUsersService users) {
            _contentManager = contentManager;
            _authorizationService = authorizationService;
            _membershipService = membershipService;
            _routeCollection = routeCollection;
            _license = license;
            _users = users;
            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }

        public ILogger Logger { get; set; }
        public Localizer T { get; set; }

        public void SetCapabilities(XElement options) {
            const string manifestUri = "http://schemas.expedit.com.au/services/manifest/license";
            options.SetElementValue(XName.Get("supportsSlug", manifestUri), "Yes");
        }

        public void Process(XmlRpcContext context) {
            var urlHelper = new UrlHelper(context.ControllerContext.RequestContext, _routeCollection);
            if (context.Request.MethodName == "license.renewSession")
            {
                var result = renewSession(
                    Convert.ToString(context.Request.Params[0].Value),
                    context._drivers);
                context.Response = new XRpcMethodResponse().Add(result);
            }
            else if (context.Request.MethodName == "license.getContactInfo")
            {
                var result = getContactInfo(
                    Convert.ToString(context.Request.Params[0].Value),
                    context._drivers);
                context.Response = new XRpcMethodResponse().Add(result);
            }
        }

        private string getContactInfo(string license, IEnumerable<IXmlRpcDriver> drivers)
        {
            var l = license.Decrypt<SessionRequest>(LicenseService.KEY_PRIVATE_DEFAULT);
            if (!l.Nonce.CheckNonce())
                throw new Orchard.OrchardCoreException(T("Your machine's date and time is out of sync with the credential service."));
            IUser user = validateUser(l.Username, l.Password);
            foreach (var driver in drivers)
                driver.Process(l.Username);
            var m = _license.GetContactInfo(l);
            m.Password = null;
            m.AuthorisedByCompanyID = _users.ApplicationCompanyID;
            return m.SignAndSerialize(LicenseService.KEY_PRIVATE_DEFAULT);
        }

        private string renewSession(string license, IEnumerable<IXmlRpcDriver> drivers)
        {
            var sr = license.Decrypt<SessionRequest>(LicenseService.KEY_PRIVATE_DEFAULT);
            if (!sr.Nonce.CheckNonce())
                throw new Orchard.OrchardCoreException(T("Your machine's date and time is out of sync with the credential service."));
            IUser user = validateUser(sr.Username, sr.Password);
            ////_authorizationService.CheckAccess(Permissions.DeleteBlogPost, user, blogPost);
            foreach (var driver in drivers)
                driver.Process(sr.Username);
            //HttpUtility.UrlEncode
            var m = _license.RenewSession(sr);
            if (m == null)
                return null;
            m.Password = null;
            m.AuthorisedByCompanyID = _users.ApplicationCompanyID;
            if (sr.RequestSessionNonce.HasValue)
            {
                m.EncryptedSessionExpiry = DateTime.UtcNow.AddDays(1);
                m.ResponseSessionKey = CryptographyHelper.GenerateKey(256);
                m.ResponseSessionNonce = GuidHelper.NewComb();
                CacheHelper.AddToCache<ISessionEncrypted>(() => { return m; }, sr.RequestSessionNonce.Value.ToNonce(), new TimeSpan(24, 0, 0)); //24 Hours                    
                m.Reference = CryptographyHelper.EncryptSymmetric<ISessionEncrypted>(m, sr.RequestSessionKey, sr.RequestSessionNonce.Value.ToNonce());
            }
            m.PrivateKey = null;
            m.PublicKey = null;
            //m.Nonce = null; NEED THIS
            m.RequestSessionKey = null;
            m.RequestSessionNonce = null;
            m.ResponseSessionKey = null;
            m.ResponseSessionNonce = null;
            return m.SignAndSerialize(LicenseService.KEY_PRIVATE_DEFAULT);
        }

        private IUser validateUser(string userName, string password) {
            IUser user = _membershipService.ValidateUser(userName, password);
            if (user == null) {
                throw new Orchard.OrchardCoreException(T("The username or e-mail or password provided is incorrect."));
            }

            return user;
        }

        
    }
}
