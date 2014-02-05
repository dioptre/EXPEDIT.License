using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EXPEDIT.License.ViewModels
{
    public class LicenseViewModel : EXPEDIT.Licence.Models.ILicenceSession
    {
        public Guid ContactID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid LicenseID { get; set; }

        public Guid? SessionID { get; set; }
        public string MachineHash { get; set; }
        public string SessionPublicKey { get; set; }
        public DateTime SessionCreated { get; set; }
        public string SessionNonce { get; set; }
        public string SessionKey { get; set; }

        public string ResponseSigned { get; set; }
        public Guid? ResponseCompanyID { get; set; }
        public Guid? ResponseSessionID { get; set; }
        public string ResponseSessionNonce { get; set; }
        public string ResponseSessionKey { get; set; }
        public string ResponseSessionPublicKey { get; set; }
    }
}