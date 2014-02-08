using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using EXPEDIT.Licence.Models;
using Newtonsoft.Json;

namespace EXPEDIT.License.ViewModels
{
    [JsonObject]
    public class LicenseViewModel : SessionRequest, ILicenceSession, ILicence, ISignature
    {
        public Guid? SessionID { get; set; }
        public string SessionHash { get; set; }
        public string MachineHash { get; set; }
        public string UserHash { get; set; }
        public string Reference { get; set; }
        public Guid? ProductID { get; set; }
        public Guid? LicenseID { get; set; }
        public Guid? ContactID { get; set; }
        public Guid? CompanyID { get; set; }
        public Dictionary<Guid, string> MemberRoles { get; set; }
        public Dictionary<Guid, string> MemberCompanies { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PublicKey { get; set; }
        [JsonIgnore]
        public string PrivateKey { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public string Nonce { get; set; }
        public string Signature { get; set; }
        public Guid? AuthorisedByCompanyID { get; set; }

      
    }
}