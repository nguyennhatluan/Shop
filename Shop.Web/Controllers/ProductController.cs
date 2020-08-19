using AutoMapper;
using Shop.Common;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        // GET: Product
        //public ActionResult Detail(int id)
        //{
        //    return View();
        //}

        public ActionResult Category(int id,int page=1, string sort="")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int maxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            int totalRow = 0;
            var listProduct = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, out totalRow,sort);

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
            var imapper = config.CreateMapper();
            var listProductViewModel = imapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(listProduct);

            var productCategory = _productCategoryService.GetById(id);
            config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductCategory, ProductCategoryViewModel>(); });
            imapper = config.CreateMapper();

            var productCategoryViewModel = imapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
            ViewBag.productCategory = productCategoryViewModel;
            PaginationSet<ProductViewModel> productPaginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = listProductViewModel,
                TotalCount = totalRow,
                Page = page,
                TotalPages = (int)Math.Ceiling((double)totalRow / pageSize),
                MaxPage = maxPage
            };
            return View(productPaginationSet);
        }
        [HttpGet]
        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetListProductByName(keyword);
            

            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet) ;
        }
        [HttpPost]
        public JsonResult LoadData(int pageIndex,int pageSize,string strSearch)
        {
            var model = _productService.LoadData(pageIndex, pageSize, strSearch);
            int count = model.Count();
            return Json(new
            {
                data = model,
                count=count
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int id)
        {
            var model = _productService.GetById(id);

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();

            });
            var imapper = config.CreateMapper();
            var listProductViewModel = imapper.Map<Product, ProductViewModel>(model);

            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(listProductViewModel.MoreImage);
            IEnumerable<Tag> listTags = _productService.GetListTagByProductId(id);
            var tags = imapper.Map<IEnumerable<Tag>,IEnumerable<TagViewModel>>(listTags);
            ViewBag.MoreImages = listImages;
            ViewBag.Tags = tags;
            return View(listProductViewModel);
        }

        public ActionResult GetListProductByTag(string tagId,int page=1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var products = _productService.GetListProductByTag(tagId, page, pageSize,out totalRow);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
            });
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var imapper = config.CreateMapper();
            var listProductViewModel = imapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(products);
            ViewBag.Tag = imapper.Map<Tag, TagViewModel>(_productService.GetTag(tagId));
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = listProductViewModel,
                TotalCount = totalRow,
                TotalPages = totalPage,
                Page = page
            };
            return View(paginationSet);
        }
    }
}