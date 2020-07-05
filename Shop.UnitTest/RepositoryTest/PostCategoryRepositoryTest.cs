using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;

namespace Shop.UnitTest.RepositoryTest
{
    /// <summary>
    /// Summary description for PostCategoryRepositoryTest
    /// </summary>
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        IDbFactory dbFactory;
        IPostCategoryRepository objRepository;
       //ProductCategoryRepository obj;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new PostCategoryRepository(dbFactory);
          //obj = new ProductCategoryRepository(dbFactory);

            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            PostCategory postCategory = new PostCategory();
            postCategory.Name = "post_category_name";
            postCategory.Alias = "post_category_name";
            postCategory.Status = true;
            postCategory.CreatedDate = DateTime.Now;
            var result = objRepository.Add(postCategory);

            //ProductCategory productCategory = new ProductCategory();
            //productCategory.Name = "name";
            //productCategory.Alias = "alias";
            //productCategory.Status = true;
            //productCategory.CreatedDate = DateTime.Now;
            //var result = obj.Add(productCategory);

            //chú ý phải thêm connectionstring vào appconfig của unitest
            unitOfWork.Commit();
            //var a = result.ID;
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }

       

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

       
    }
}
