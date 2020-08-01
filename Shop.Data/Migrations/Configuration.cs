namespace Shop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Shop.Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Shop.Data.ShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Shop.Data.ShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ShopDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "nhatluan",
            //    Email = "nhatluan98@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Nguyen Nhat Luan"

            //};

            //manager.Create(user, "123654$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("tedu.international@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            CreateProductCategorySample(context);
            CreateSlide(context);

        }
        private void CreateProductCategorySample(Shop.Data.ShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name="Điện lạnh",Alias="dien-lanh",Status=true, CreatedDate = DateTime.Now },
                 new ProductCategory() { Name="Viễn thông",Alias="vien-thong",Status=true, CreatedDate = DateTime.Now },
                  new ProductCategory() { Name="Đồ gia dụng",Alias="do-gia-dung",Status=true, CreatedDate = DateTime.Now },
                   new ProductCategory() { Name="Mỹ phẩm",Alias="my-pham",Status=true, CreatedDate = DateTime.Now }
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }

        }

        private void CreateSlide(Shop.Data.ShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide(){Name="Slide01",Description="",
                        Image="/Assets/client/images/bag.jpg",
                        Url="#",
                        DisplayOrder=1,
                        Status=true,
                        Content=@"<h2>FLAT 50% 0FF</h2>
								<label>FOR ALL PURCHASE <b>VALUE</b></label>
								<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>					
								<span class=""on-get"">GET NOW</span>"},
                    new Slide(){Name="Slide02",Description="",
                        Image="/Assets/client/images/bag1.jpg",
                        Url="#",
                        DisplayOrder=1,
                        Status=true,
                        Content=@"<h2>FLAT 50% 0FF</h2>
								<label>FOR ALL PURCHASE <b>VALUE</b></label>
								<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>					
								<span class=""on-get"">GET NOW</span>"},
                    new Slide(){Name="Slide01",Description="",
                        Image="/Assets/client/images/bag.jpg",
                        Url="#",
                        DisplayOrder=1,
                        Status=true,
                        Content=@"<h2>FLAT 50% 0FF</h2>
								<label>FOR ALL PURCHASE <b>VALUE</b></label>
								<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>					
								<span class=""on-get"">GET NOW</span>"}
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }
    }
}
