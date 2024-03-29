using System.Web.Mvc;
using Orchard.Localization;
using Orchard;
using EXPEDIT.License.Services;
using Orchard.Themes;
using NKD.Helpers;
using System;
using Orchard.DisplayManagement;
using System.Web;
using Orchard.UI.Navigation;
using Orchard.Search.Models;
using Orchard.Search.Services;
using Orchard.Search.ViewModels;
using Orchard.Settings;
using Orchard.UI.Notify;
using Orchard.Indexing;
using Orchard.Logging;
using Orchard.Collections;
using Orchard.ContentManagement;
using System.Collections.Generic;
using System.Linq;
using Orchard.Mvc;

namespace EXPEDIT.License.Controllers {
    
    [Themed]
    public class UserController : Controller {
        public IOrchardServices Services { get; set; }
        private ILicenseService _License { get; set; }
        public ILogger Logger { get; set; }
        private readonly IContentManager _contentManager;
        private readonly ISiteService _siteService;

        public UserController(
            IOrchardServices services,
            ILicenseService License,
            IContentManager contentManager,
            ISiteService siteService,
            IShapeFactory shapeFactory
            )
        {
            _License = License;
            Services = services;
            T = NullLocalizer.Instance;

            _contentManager = contentManager;
            _siteService = siteService;
        }

        public Localizer T { get; set; }

        ///// <summary>
        ///// Redirect/Route Utility
        ///// </summary>
        ///// <returns></returns>
        //[ValidateInput(false)]
        ////[Themed(false)]
        //public ActionResult Go(string id) 
        //{
        //    var redirect = _License.GetRedirect(id);
        //    if (!string.IsNullOrWhiteSpace(redirect))
        //        return new RedirectResult(redirect);
        //    return new HttpNotFoundResult();
        //}

        ///// <summary>
        ///// Download a file (can use a sqlfilestream eventually) TODO:
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="name"></param>
        ///// <param name="contactid"></param>
        ///// <returns></returns>
        //[ValidateInput(false)]
        ////[ValidateAntiForgeryToken]
        ////[Themed(false)]
        //[Authorize]
        //public ActionResult Download(string id)
        //{

        //    var file = _License.GetDownload(id, Request.GetIPAddress());
        //    if (file != null)
        //        return new NKD.Handlers.FileGeneratingResult(string.Format("{0}-{1}-{2}", id, NKD.Helpers.DateHelper.NowInOnlineFormat, file.FileName).Trim(), "application/octet", stream => new System.IO.MemoryStream(file.FileBytes).WriteTo(stream));
        //    return new HttpNotFoundResult();
        //}

        ///// <summary>
        ///// Download a file (can use a sqlfilestream eventually) TODO:
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="name"></param>
        ///// <param name="contactid"></param>
        ///// <returns></returns>
        //[ValidateInput(false)]
        ////[ValidateAntiForgeryToken]
        ////[Themed(false)]
        //[Authorize]
        //public ActionResult File(string id)
        //{
        //    var file = _License.GetFile(new Guid(id));
        //    if (file != null)
        //        return new NKD.Handlers.FileGeneratingResult(string.Format("{0}-{1}-{2}", id, NKD.Helpers.DateHelper.NowInOnlineFormat, file.FileName).Trim(), "application/octet", stream => new System.IO.MemoryStream(file.FileBytes).WriteTo(stream));
        //    return new HttpNotFoundResult();
        //}



        //[ValidateInput(false)]
        //[Authorize]
        ////[ValidateAntiForgeryToken]
        //[Themed(true)]
        //public ActionResult GetInvoice(string id)
        //{
        //    return Download(string.Format("{0}",_content.GetInvoice(new Guid(id), Request.GetIPAddress())));
        //}

        //[ValidateInput(false)]
        //[Authorize]
        ////[ValidateAntiForgeryToken]
        //[Themed(true)]
        //public ActionResult GetOrderInvoice(string id)
        //{
        //    return Download(string.Format("{0}", _content.GetOrderInvoice(new Guid(id), Request.GetIPAddress())));
        //}

        //[ValidateInput(false)]
        //[Authorize]
        //[Themed(false)]
        //public JsonResult GetCountries(string id)
        //{                       
        //    return Json(_content.GetCountries(id), JsonRequestBehavior.AllowGet);
        //}


        //dynamic Shape { get; set; }
        //[Themed(true)]
        //public ActionResult Search(PagerParameters pagerParameters, string q = "")
        //{
        //    var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);
        //    var searchFields = Services.WorkContext.CurrentSite.As<SearchSettingsPart>().SearchedFields;

        //    IPageOfItems<ISearchHit> searchHits = new PageOfItems<ISearchHit>(new ISearchHit[] { });
        //    try
        //    {
        //        searchHits = _searchService.Query(q, pager.Page, pager.PageSize,
        //                                          Services.WorkContext.CurrentSite.As<SearchSettingsPart>().Record.FilterCulture,
        //                                          searchFields,
        //                                          searchHit => searchHit);
        //    }
        //    catch (Exception exception)
        //    {
        //        Logger.Error(T("Invalid search query: {0}", exception.Message).Text);
        //        Services.Notifier.Error(T("Invalid search query: {0}", exception.Message));
        //    }

        //    var list = Shape.List();
        //    var offset = (pager.Page - 1) * pager.PageSize + 1;
        //    var dbSearch = _License.GetSearchResults(q, null, offset, pager.PageSize);
        //    var dbCount = dbSearch.Count();
        //    foreach (var hit in dbSearch)
        //    {
        //        list.Add(hit);
        //    }

        //    var foundIds = searchHits.Select(searchHit => searchHit.ContentItemId).ToList();

        //    // ignore search results which content item has been removed or unpublished
        //    var foundItems = _contentManager.GetMany<IContent>(foundIds, VersionOptions.Published, new QueryHints()).ToList();
        //    foreach (var contentItem in foundItems)
        //    {
        //        list.Add(_contentManager.BuildDisplay(contentItem, "Summary"));                
        //    }
        //    searchHits.TotalItemCount -= foundIds.Count() - foundItems.Count();

        //    var pagerShape = Shape.Pager(pager).TotalItemCount(searchHits.TotalItemCount);

        //    var searchViewModel = new SearchViewModel
        //    {
        //        Query = q,
        //        TotalItemCount = searchHits.TotalItemCount + dbCount,
        //        StartPosition = offset,
        //        EndPosition = pager.Page * pager.PageSize > (searchHits.TotalItemCount + dbCount) ? (searchHits.TotalItemCount + dbCount) : pager.Page * pager.PageSize, //TODO: Hack, fix
        //        ContentItems = list,
        //        Pager = pagerShape
        //    };

        //    //todo: deal with page requests beyond result count

        //    return View(searchViewModel);
        //}

        //[ValidateInput(false)]
        //public ActionResult Refer(string id, string name)
        //{
        //    try
        //    {
        //        _content.UpdateAffiliate(null, new Guid(id), Request.GetIPAddress(), true);
        //    }
        //    catch { }
        //    if (string.IsNullOrWhiteSpace(name))
        //        return new RedirectResult(VirtualPathUtility.ToAbsolute("~/"));
        //    else
        //    {
        //        try
        //        {
        //            return new RedirectResult(VirtualPathUtility.ToAbsolute(string.Format("~/{0}", Server.UrlDecode(name)).ToLowerInvariant().Replace("/refer/", "/")));
        //        }
        //        catch
        //        {
        //            return HttpNotFound();
        //        }
        //    }
        //}

        //[Themed(false)]
        //[ValidateInput(false)]
        //public ActionResult Referral(string id, string name)
        //{
        //    try
        //    {
        //        return new JsonHelper.JsonNetResult(_content.UpdateAffiliate(null, null, Request.GetIPAddress()).AffiliateID, JsonRequestBehavior.AllowGet);
        //    }
        //    catch 
        //    {
        //        return new JsonHelper.JsonNetResult(string.Empty, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [Themed(false)]
        [ValidateInput(false)]
        public ActionResult Referral(string id, string name)
        {
            try
            {
                return new JsonHelper.JsonNetResult(_License.GetTrustTree(), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonHelper.JsonNetResult(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
