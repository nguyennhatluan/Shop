using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService,IPostCategoryService postCategoryService) : base(errorService)
        {
            _postCategoryService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                //var listCategory = _postCategoryService.GetAll();

                //HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCategory);


                //return response;
                var listCategory = _postCategoryService.GetAll();
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Post, PostViewModel>();
                    cfg.CreateMap<PostTag, PostTagViewModel>();
                    cfg.CreateMap<Tag, TagViewModel>();
                });
                IMapper mapper = config.CreateMapper();
               // var listPostCategoryVm = Mapper.Map<List<PostCategoryViewModel>>(listCategory);
                var listPostCategoryVm = mapper.Map<List<PostCategoryViewModel>>(listCategory);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVm);

                return response;
            });
        }

        public HttpResponseMessage Post(HttpRequestMessage request,PostCategory postCategory)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var category = _postCategoryService.Add(postCategory);
                    _postCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }
                return response;
            });
            
        }

        public HttpResponseMessage Put(HttpRequestMessage request, PostCategory postCategory)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>
            {
                
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Update(postCategory);
                    _postCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>{
                
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}