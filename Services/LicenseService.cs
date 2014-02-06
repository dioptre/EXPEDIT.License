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
#if XODB
using XODB.Module.BusinessObjects;
#else
using EXPEDIT.Utils.DAL.Models;
#endif
using XODB.Services;
using Orchard.Media.Services;
using EXPEDIT.License.ViewModels;
using EXPEDIT.License.Helpers;
using Orchard.DisplayManagement;
using ImpromptuInterface;
using XODB.Models;
using EXPEDIT.License.ViewModels;
using EXPEDIT.Licence.Models;

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
                    var d = new XODBC(_users.ApplicationConnectionString, null, false);
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public ILicenceSession RenewSession(LicenseViewModel m)
        {
            return null;
        }

        public ILicence GetContactInfo(LicenseViewModel m)
        {
            return null;
        }

       
    }
}
