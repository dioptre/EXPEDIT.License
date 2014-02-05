using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Orchard;
using System.ServiceModel;
using XODB.Module.BusinessObjects;

namespace EXPEDIT.License.Services
{
     [ServiceContract]
    public interface ILicenseService : IDependency 
    {
         [OperationContract]
         string GetTrustTree(Guid? trustTreeRootID = default(Guid?));

    }
}