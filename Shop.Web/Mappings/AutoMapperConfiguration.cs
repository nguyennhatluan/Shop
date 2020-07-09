using AutoMapper;
using Shop.Model.Models;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            //Mapper.CreateMap<Post, PostViewModel>();
            //Mapper.CreateMap<PostTag, PostTagViewModel>();
            //Mapper.CreateMap<Tag, TagViewModel>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<PostTag, PostTagViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
            });

            IMapper mapper = config.CreateMapper();
        }
    }
}