﻿using AutoMapper;
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
using System.Web.Script.Serialization;

namespace Shop.Web.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;
        public ProductController(IErrorService errorService, IProductService productService, IProductCategoryService productCategoryService) : base(errorService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }
        [Route("getallcategory")]
        [HttpGet]
        public HttpResponseMessage GetAllCategory(HttpRequestMessage request)
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
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyWord, int pageSize, int page)
        {
            return CreateHttpResponse(request, () =>
            {
                var listProduct = _productService.GetAll(keyWord);
                var totalCount = listProduct.Count();
                var query = listProduct.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
                IMapper imapper = config.CreateMapper();
                var responseData = imapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);
                var paginationSet = new PaginationSet<ProductViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize)
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;

            });
        }
        [Route("getbyid")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage requestMessage,int id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _productService.GetById(id);
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
                IMapper imapper = config.CreateMapper();
                var responseData = imapper.Map<Product, ProductViewModel>(model);
                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        
        //[Route("getall")]
        //[HttpGet]
        //[AllowAnonymous]
        //public HttpResponseMessage GetAll(HttpRequestMessage request)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        var listProduct = _productService.GetAll();
        //        var totalCount = listProduct.Count();

        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
        //        IMapper imapper = config.CreateMapper();
        //        var responseData = imapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(listProduct);

        //        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);

        //        return response;

        //    });
        //}

        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productViewModel)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest);
                }
                else
                {
                    var newProduct = new Product();
                    
                    newProduct.UpdateProduct(productViewModel);
                    _productService.Add(newProduct);
                    _productService.Save();
                    var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
                    IMapper imapper = config.CreateMapper();
                    var responseData = imapper.Map<Product, ProductViewModel>(newProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }
        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage requestMessage, ProductViewModel productViewModel)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(requestMessage, () =>
            {
                if (!ModelState.IsValid)
                {
                    response = requestMessage.CreateResponse(HttpStatusCode.BadRequest);
                }
                else
                {
                    var product = _productService.GetById(productViewModel.ID);
                    product.UpdateProduct(productViewModel);
                    _productService.Update(product);
                    _productService.Save();
                    var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
                    IMapper imapper = config.CreateMapper();
                    var responseData = imapper.Map<Product, ProductViewModel>(product);
                    response = requestMessage.CreateResponse(HttpStatusCode.Created, responseData);


                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage requestMessage, int id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                _productService.Delete(id);
                _productService.Save();

                return requestMessage.CreateResponse(HttpStatusCode.Created);
            });
        }
        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage requestMessage, string stringID)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var listId = new JavaScriptSerializer().Deserialize<List<int>>(stringID);
                foreach(int id in listId)
                {
                    _productService.Delete(id);
                }
                _productService.Save();
                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.Created, listId.Count);
                return response;
            });
        }
        //private IProductCategoryService _productCategoryService;
        //private IProductService _productService;

        //public ProductController(IErrorService errorService,IProductCategoryService productCategoryService, IProductService productService) : base(errorService)
        //{
        //    _productCategoryService = productCategoryService;
        //    _productService = productService;
        //}
        //[Route("getall")]
        //[HttpGet]
        //public HttpResponseMessage GetAll(HttpRequestMessage requestMessage,string keyWord,int page,int pageSize)
        //{
        //    string[] includes = { "ProductCategories" };
        //    return CreateHttpResponse(requestMessage, () =>
        //    {
        //        var listProduct = _productService.GetAll(includes, keyWord);
        //        var totalCount = listProduct.Count();
        //        var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
        //        var query = listProduct.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
        //        IMapper mapper = config.CreateMapper();
        //        var responseData = mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);
        //        var paginationSet = new PaginationSet<ProductViewModel>()
        //        {
        //            Items=responseData,
        //            TotalCount=totalCount,
        //            TotalPages=totalPages,
        //            Page=page
        //        };
        //        var response = requestMessage.CreateResponse(HttpStatusCode.OK, paginationSet);
        //        return response;
        //    });
        //}
        
    }
}