using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;

        public HomeController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Category()
        {
            var listProductCategories = _productCategoryService.GetAll();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductCategory, ProductCategoryViewModel>(); });
            IMapper imapper = config.CreateMapper();
            var listProductCategoriesViewModel = imapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listProductCategories);
            
            return PartialView(listProductCategoriesViewModel);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }
    }
}