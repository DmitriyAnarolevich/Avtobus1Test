using ShortUrlService.BLL.Models;
using ShortUrlService.BLL.Services;
using ShortUrlService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShortUrlService.Controllers
{
    public class HomeController : Controller
    {
        private UrlService urlService;

        public HomeController()
        {
            urlService = new UrlService();
        }
        
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                try
                {
                    var urlHash = id.Value;

                    var fullUrl = urlService.GetFullUrlByHashCode(urlHash);

                    urlService.PlusRedirectionByHashCode(urlHash);

                    return Redirect(fullUrl);
                }
                catch(Exception ex)
                {
                    return new HttpNotFoundResult(ex.Message);

                }
            }
            

            var urlDtos = urlService.GetAllUrls();
            var model = MvcApplication.UrlDTOToUrlViewModel.CreateMapper().Map<List<UrlViewModel>>(urlDtos);

            return View(model);
        }

        public ActionResult Delete(long id)
        {
            urlService.DeleteUrl(id);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return View(new UrlViewModel());
            }

            var urlDto = urlService.GetUrl(id.Value);

            var mapper = MvcApplication.UrlDTOToUrlViewModel.CreateMapper();

            var urlViewModel = mapper.Map<UrlViewModel>(urlDto);

            return View(urlViewModel);
        }

        [HttpPost]
        public ActionResult Edit(UrlViewModel url)
        {
            var mapper = MvcApplication.UrlViewModelToUrlDTO.CreateMapper();

            var urlDto = mapper.Map<UrlDTO>(url);

            urlService.CreateOrUpdate(urlDto);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            urlService.Dispose();
            base.Dispose(disposing);
        }
    }
}