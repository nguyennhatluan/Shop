using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Infrastructure.Extensions;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        IProductCategoryService _productCategoryService;
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            _productCategoryService = productCategoryService;
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request,string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listProductCategory = _productCategoryService.GetAll(keyword);
                totalRow = listProductCategory.Count();
                var query = listProductCategory.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductCategory, ProductCategoryViewModel>(); });
                IMapper imapper = config.CreateMapper();
                var responseData = imapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);
                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }
        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetById(id);
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductCategory, ProductCategoryViewModel>(); });
                IMapper imapper = config.CreateMapper();
                var responseData = imapper.Map<ProductCategory, ProductCategoryViewModel>(model);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                
                var listProductCategory = _productCategoryService.GetAll();
                
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductCategory, ProductCategoryViewModel>(); });
                IMapper imapper = config.CreateMapper();
                var responseData = imapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listProductCategory);
                
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(productCategoryVm);
                    _productCategoryService.Add(newProductCategory);
                    _productCategoryService.Save();
                    var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductCategory, ProductCategoryViewModel>(); });
                    IMapper imapper = config.CreateMapper();
                    var responseData = imapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("Update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbProductCategory = _productCategoryService.GetById(productCategoryVm.ID);
                    dbProductCategory.UpdateProductCategory(productCategoryVm);
                    _productCategoryService.Update(dbProductCategory);
                    _productCategoryService.Save();
                    var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductCategory, ProductCategoryViewModel>(); });
                    IMapper imapper = config.CreateMapper();
                    var responseData = imapper.Map<ProductCategory, ProductCategoryViewModel>(dbProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }
    }
}