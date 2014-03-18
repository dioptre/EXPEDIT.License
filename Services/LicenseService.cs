using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.FileSystems.Media;
using Orchard.Localization;
using Orchard.Security;
using Orchard.Settings;
using Orchard.Validation;
using Orchard;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Transactions;
using Orchard.Messaging.Services;
using Orchard.Logging;
using Orchard.Tasks.Scheduling;
using Orchard.Data;
using NKD.Module.BusinessObjects;
using NKD.Services;
using Orchard.Media.Services;
using EXPEDIT.License.ViewModels;
using EXPEDIT.License.Helpers;
using Orchard.DisplayManagement;
using ImpromptuInterface;
using NKD.Models;
using EXPEDIT.License.ViewModels;
using CNX.Shared.Models;

namespace EXPEDIT.License.Services {
    
    [UsedImplicitly]
    public class LicenseService : ILicenseService {
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly IMessageManager _messageManager;
        private readonly IScheduledTaskManager _taskManager;
        private readonly IUsersService _users;
        private readonly IMediaService _media;
        public ILogger Logger { get; set; }

        public LicenseService(
            IContentManager contentManager, 
            IOrchardServices orchardServices, 
            IMessageManager messageManager, 
            IScheduledTaskManager taskManager, 
            IUsersService users, 
            IMediaService media)
        {
            _orchardServices = orchardServices;
            _contentManager = contentManager;
            _messageManager = messageManager;
            _taskManager = taskManager;
            _media = media;
            _users = users;
            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public Localizer T { get; set; }

        //There are 2 methods to check for a licence
        //#1 Query Blockchain
        //#2 Submission with exact package signed on return
        //Include pubKey in SW

        public string GetTrustTree(Guid? trustTreeRootID = default(Guid?))
        {
            try
            {
                var application = _users.ApplicationID;
                using (new TransactionScope(TransactionScopeOption.Suppress))
                {
                    var d = new NKDC(_users.ApplicationConnectionString, null, false);
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public ISessionEncrypted RenewSession(ISessionEncrypted m)
        {
            //namecoind -server -rpcuser=user -rpcpassword=pwd --rpcallowip=127.0.0.1
            //Do checks here                       
            return null;
        }

        public ILicence GetContactInfo(ILicence m)
        {
            try
            {
                m.ContactID = _users.GetContactID(m.Username);
                m.CompanyID = _users.GetDefaultCompanyID(m.ContactID);
                return m;
            }
            catch
            {
                return null;
            }
        }

       
    }
}
