using AutoMapper;
using Shop.Common;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Shop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        IProductService _productService;

        public ShoppingCartController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            
            if (Session[CommonConstants.SessionCart] == null)
            {
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            }
            return View();
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            }
            var cart = (List<ShoppingCartViewModel>)(Session[CommonConstants.SessionCart]);
            return Json(new
            {
                data = cart,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Add(int productId)
        {
            
            if (Session[CommonConstants.SessionCart] == null)
            {
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>(); 
            }
            
            else
            {
                var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
                if (cart.Any(x => x.ProductId == productId))
                {
                    foreach(var item in cart)
                    {
                        if (item.ProductId == productId)
                        {
                            item.Quantity += 1;
                        }
                    }
                }
                else
                {
                    var product = _productService.GetById(productId);

                    var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
                    var imapper = config.CreateMapper();
                    var productViewModel = imapper.Map<Product, ProductViewModel>(product);

                    var shoppingCart = new ShoppingCartViewModel()
                    {
                        ProductId = productId,
                        Product = productViewModel,
                        Quantity = 1
                    };
                    cart.Add(shoppingCart);
                }
                Session[CommonConstants.SessionCart] = cart;
            }
            return Json(new
            {
                status=true
            });

        }

        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)(Session[CommonConstants.SessionCart]);
            if (cart != null)
            {
                cart.RemoveAll(x => x.ProductId == productId);
                Session[CommonConstants.SessionCart] = cart;
                return Json(new
                {
                    status = true
                });
                
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [HttpPost]
        public JsonResult Update(string item)
        {
            
            var itemInCart = new JavaScriptSerializer().Deserialize<ShoppingCartViewModel>(item);
            var cart = (List<ShoppingCartViewModel>)(Session[CommonConstants.SessionCart]);
            foreach(var jtem in cart)
            {
                if (jtem.ProductId == itemInCart.ProductId)
                {
                    jtem.Quantity = itemInCart.Quantity;
                }
            }
            Session[CommonConstants.SessionCart] = cart;

            return Json(new
            {

            });
        }
    }
}