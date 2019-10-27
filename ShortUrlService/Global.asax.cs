using AutoMapper;
using ShortUrlService.BLL.Models;
using ShortUrlService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShortUrlService
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static MapperConfiguration urlViewModelToUrlDTO;
        public static MapperConfiguration UrlViewModelToUrlDTO
        {
            get
            {
                if (urlViewModelToUrlDTO == null)
                {
                    urlViewModelToUrlDTO = new MapperConfiguration(cfg => cfg.CreateMap<UrlViewModel, UrlDTO>());
                }
                return urlViewModelToUrlDTO;
            }
        }

        private static MapperConfiguration urlDTOToUrlViewModel;
        public static MapperConfiguration UrlDTOToUrlViewModel
        {
            get
            {
                if (urlDTOToUrlViewModel == null)
                {
                    urlDTOToUrlViewModel = new MapperConfiguration(cfg => cfg.CreateMap<UrlDTO, UrlViewModel>());
                }
                return urlDTOToUrlViewModel;
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
