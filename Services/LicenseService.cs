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

using Orchard.DisplayManagement;
using ImpromptuInterface;
using NKD.Models;
using CNX.Shared.Models;

namespace EXPEDIT.License.Services {
    
    [UsedImplicitly]
    public class LicenseService : ILicenseService {

        internal static string[] WIFS_PRIVATE_CREDNEXUS = new string[] {
            "732vd1BJ2jQT1skxAzdKNRGnNpvmCTDLskD29E89ipKA7b31UdQ", //NEXUSCK711W5xQ3Pw5HBbiWTUtJLmbvGXz
            "74b5qwCmcitjRRfthEYHZHiR1aGcJPsjqDfM1UqizEAiJx4dS5N", //NEXUSCAVfEk8nLKkWGBGdERth5sKuqNBjQ
            "73xQqDGaqSdZC1Vn3PY1LFK1Ae8tBRwbdbF4Z4AKVa8W6zWmuDM"  //NEXUSCvftgFVJVknYYZk8MZkb4AaANWhnx
        };

        internal const string WIF_SITE = "5JxoMpwRD2C3WUkCTrMM2xGB1dA7CghzFfjBESm5FmjDPUa7rS6"; //MzJwgvBLtrEeoNUzBPQb57SGp2AVf8MstF
        internal const string KEY_PRIVATE_DEFAULT = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCYU4xT4bwGV9y08lLj4MOdPy2MZTTlGFiPloR1kK8z4nwtGFIxqy2DBEAuis6P9Z+Tk0S+/rBtX5QgpIPqY98SeYhCkPeTucwqTru7HG7Tdj9NZlPlnQ258gelEHSO18L7mWk+JYl6f6u8kFCaDWwG+KlV5WWXExQLA5dXqjrX+WCEE8UZ5eb7zwFZvenAgGgnmH2WyJC3qOlrhdHbPvTBoCbG3KAouV/cUn24a9TTQCF3DHTi4FPK+hyKHctG73dfQm5pJBHKDdqJbQ0Va0c5dDtu5AojGzBSnMImKNsPKr63LWuO64FMKGl11FaNZe1mIehMa4jsY1ffpyXSLYunAgMBAAECggEAZAx0HeAlJDFvWDXVNbE6Kj0FyLHspRBxkpX1GFbYjIaUsvXHfrIE6YnQMgGfnLRihIZ039HexWfCnhIQRtIkATlrwvT+d7vQGnWuHj6VmDSRbV/peOXHzzrlxIfjVrLmcWSY2GXFP309qlNLbXOlYYrPhghuymSQhI9uRvkbPyCacVFA/gWi8CsdK7ip0YrM/wOLM8sI5+lM9+kQueooYwSDz3EHCrbPCd7r+3qKk5PHNNECeCBytT/jw3j6ZIzLvjNGhrdQGMNhRxkB3C0hFJFhqq4xI6yOs6Q/XztUwYv+4lgS4K2jeEip4fQJW7rQiSftr5bAM9Fp9oHuZFrOsQKBgQDbH+CuSlAsqOpdqgxAu7+leMoIVS3Jujw/iTwGsdjYA8a0zqg18kHS8heDs7tZtbxuz44bmfTcVgCDRYOUamwrbmoAHhtmP9eLYGGGlKqQbeOV48qPywVZFChGO3QlmZ45CtcTKvTuZgxTR6aKwQ0xXOoAa0In7u3JuHTtZjstSQKBgQCx9e7iUA9U75148DeDGPtck0qNZ/aKGKHJCNIEkh52uoaEftuT5g+U852KoS69sz2cUx/WzpdLxScHn44yGC/FgkSQzJcMd8vsn4rl8Yx1uNWXhnkwNM7VjD1VwZ413IDkh7DEI3fNEcwqk2nowDsjrFfX8ZjnOO8i8rxpYLqhbwKBgQCKImRfOxWjsbBc72/d9v1vcN/btOayfqawXvDqP381XdwL6yL7Lwbz1g2gxtLaUMjDCjDJkZpctBKKrm2uSBB8qJRGErSvFpvojw+r6VhEyCFqQjlVwGRUrXJeI+iqM1cdGopO2Quipc4rScXhPqX0cmBJd1QzHFnmilObvJCdkQKBgD/AsQGWWMe+x5UpyVlHu9TgV1btJZ83T84rQMGubwdtrv8MSzFiu7ZKx+d/8rS2351/EersO7tDN8Y9XL2JeKOzFUkiYgJvcDimtyXFMOKDgtEztXqVkHtkMBzmrfzxr6MvER5S7noipBekk85z/zu6ZAXSYUqEVPcaKnE92941AoGBALgr71ngp0O3+91K+KOnUDrKgoVtXmU9s2WMQkrNAOvvDQuv2j8zwA4rx90tCJ2J6udEuaLpuQDYwjyf2Dwl+VfAKDy4fAkctWQvJv+agSUTIw4427YbxWcHp+FRnscxoVf+j5pI10q/uW1OH4y/rB0yIlNqah0eB9y+fz2RQhs6";           

        private readonly IOrchardServices _orchardServices;
        private readonly IUsersService _users;
        private readonly IMediaService _media;
        public ILogger Logger { get; set; }

        public LicenseService(
            IOrchardServices orchardServices, 
            IUsersService users, 
            IMediaService media)
        {
            _orchardServices = orchardServices;
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
