using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductsRepository repository;
        private IOrderProcessor orderProcessor;
        private IStatesRepository stateRepo;

        public CartController(IProductsRepository repo, IOrderProcessor proc, IStatesRepository state)
        {
            repository = repo;
            orderProcessor = proc;
            stateRepo = state;
        }

        public ViewResult Checkout()
        {
            var users = stateRepo.States.ToList().Select(u => new SelectListItem
                                    {
                                        Text = u.abbreviation,
                                        Value = u.abbreviation
                                    });
            
            ShippingDetails sd = new ShippingDetails
            {
                States = users
            };
            return View(sd);
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails){
 
            
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            var allErrors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                /*
                if (shippingDetails.State == "CA")
                {
                   decimal total = cart.ComputeTotalValue();
                    
                }*/
               // orderProcessor.ProcessOrder(cart, shippingDetails);
               // cart.Clear();
                ViewBag.Data = shippingDetails;
                return View("Confirm", new Checkout
                {
                    Cart = cart,
                    ShippingDetails = shippingDetails
                });
            }
            else
            {
                var users = stateRepo.States.ToList().Select(u => new SelectListItem
                {
                    Text = u.abbreviation,
                    Value = u.abbreviation
                });

                ShippingDetails sd = new ShippingDetails
                {
                    States = users
                };
                return View(sd);
            }
        }
        [HttpPost]
        public ViewResult Confirm(Cart cart, Checkout checkout)
        {
            var shipping = checkout.ShippingDetails;
            orderProcessor.Test(cart, shipping);
            Session.Clear();
            return View("Reciept", new Checkout{ 
                Cart = cart,
                ShippingDetails = shipping
            } );
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart = cart,
                ReturnUrl = returnUrl
            });
            

        }
        //Add to cart function. Find a way to pass a textbox value to this.
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
         
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult UpdateCart(Cart cart, int productId, int quantity, string returnUrl)
        {

            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.UpdateItem(product, quantity);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
               .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
              cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult EmptyCart(Cart cart, string returnUrl)
        {
            cart.Clear();
            
            return RedirectToAction("Index", new { returnUrl });
        }


        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
       

    }
}
