﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using CNX.Shared.Models;
using Newtonsoft.Json;

namespace EXPEDIT.License.ViewModels
{
    [JsonObject]
    public class LicenseViewModel : SessionRequest, ISession, ILicence, ISignature
    {


      
    }
}