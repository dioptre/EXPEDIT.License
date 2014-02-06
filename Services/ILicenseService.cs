using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Orchard;
using System.ServiceModel;

using EXPEDIT.License.ViewModels;
using EXPEDIT.Licence.Models;

namespace EXPEDIT.License.Services
{
     [ServiceContract]
    public interface ILicenseService : IDependency 
    {
         [OperationContract]
         string GetTrustTree(Guid? trustTreeRootID = default(Guid?));

         [OperationContract]
         ILicenceSession RenewSession(LicenseViewModel m);

         [OperationContract]
         ILicence GetContactInfo(LicenseViewModel m);

    }
}