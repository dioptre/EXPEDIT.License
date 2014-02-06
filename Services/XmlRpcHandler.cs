using System;
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
using EXPEDIT.Licence.Helpers;
using EXPEDIT.Licence.Models;
using EXPEDIT.License.Helpers;
using System.Dynamic;
using ImpromptuInterface;

using EXPEDIT.License.ViewModels;

namespace EXPEDIT.License.Services {
    [UsedImplicitly]
    public class XmlRpcHandler : IXmlRpcHandler {
        private readonly IContentManager _contentManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMembershipService _membershipService;
        private readonly RouteCollection _routeCollection;
        private readonly ILicenseService _license;

        public XmlRpcHandler(IContentManager contentManager,
            IAuthorizationService authorizationService, 
            IMembershipService membershipService, 
            RouteCollection routeCollection,
            ILicenseService license) {
            _contentManager = contentManager;
            _authorizationService = authorizationService;
            _membershipService = membershipService;
            _routeCollection = routeCollection;
            _license = license;
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

        private string getContactInfo(
        string license,
        IEnumerable<IXmlRpcDriver> drivers)
        {
            var l = JsonConvert.DeserializeObject<LicenseViewModel>(license.Decrypt(ConstantsHelper.KEY_PRIVATE_DEFAULT));
            IUser user = ValidateUser(l.Username, l.Password);
            foreach (var driver in drivers)
                driver.Process(l.Username);
            return JsonConvert.SerializeObject(_license.GetContactInfo(l));
        }

        private string renewSession(
            string license,
            IEnumerable<IXmlRpcDriver> drivers) {
                var l = JsonConvert.DeserializeObject<LicenseViewModel>(license.Decrypt(ConstantsHelper.KEY_PRIVATE_DEFAULT));
            
                IUser user = ValidateUser(l.Username, l.Password);
                ////_authorizationService.CheckAccess(Permissions.DeleteBlogPost, user, blogPost);

                foreach (var driver in drivers)
                    driver.Process(l.Username);
                //HttpUtility.UrlEncode
                return JsonConvert.SerializeObject(_license.RenewSession(l));            
        }

        private IUser ValidateUser(string userName, string password) {
            IUser user = _membershipService.ValidateUser(userName, password);
            if (user == null) {
                throw new Orchard.OrchardCoreException(T("The username or e-mail or password provided is incorrect."));
            }

            return user;
        }

        
    }
}
