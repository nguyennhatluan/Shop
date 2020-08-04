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
        public ActionResult Detail(int id)
        {
            return View();
        }

        public ActionResult Category(int id,int page=1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int maxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            int totalRow = 0;
            var listProduct = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, out totalRow);
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
    }
}