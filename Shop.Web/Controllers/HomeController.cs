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
        IProductService _productService;
        ISlideService _slideService;


        public HomeController(IProductCategoryService productCategoryService,IProductService productService,ISlideService slideService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            _slideService = slideService;
        }
        [OutputCache(Duration =60,Location =System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var listSlide = _slideService.GetAll();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Slide, SlideViewModel>(); });
            IMapper imapper = config.CreateMapper();
            var listSlideViewModel = imapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(listSlide);
            var listProduct = _productService.GetAll();
            
            config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
            imapper = config.CreateMapper();
            var listProductViewModel = imapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(listProduct);
            var listLatestProduct = imapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(_productService.GetLatestProduct(3));
            var listHotProduct = imapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(_productService.GetHotProduct(3));
            var homeViewModel = new HomeViewModel()
            {
                Slides = listSlideViewModel,
                Products = listProductViewModel
            };
            homeViewModel.LatestProducts = listLatestProduct;
            homeViewModel.HotProducts = listHotProduct;
            return View(homeViewModel);
        }
        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }
        [ChildActionOnly]
        [OutputCache(Duration =3600)]
        public ActionResult Category()
        {
            var listProductCategories = _productCategoryService.GetAll();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductCategory, ProductCategoryViewModel>(); });
            IMapper imapper = config.CreateMapper();
            var listProductCategoriesViewModel = imapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listProductCategories);
            
            return PartialView(listProductCategoriesViewModel);
        }

        

        [ChildActionOnly]
        [OutputCache(Duration =3600)]
        public ActionResult Footer()
        {
            return PartialView();
        }
    }
}