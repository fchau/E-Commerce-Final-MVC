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
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        private IProductsRepository repository;
        private ICategoryRepository catRepo;
        public int PageSize = 4;

        public ProductController(IProductsRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.repository = productRepository;
            this.catRepo = categoryRepository;
        }

 

        public ViewResult List(string category, int? id, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel();
            if (id == null)
            {
                model.Products = repository.Products.OrderBy(p => p.ProductID)
                  .Where(p => (category == null || p.Category == category) || (p.CategoryID == id))
                  .Skip((page - 1) * PageSize)
                  .Take(PageSize);
                model.PagingInfo = new PagingInfo
                   {
                       CurrentPage = page,
                       ItemsPerPage = PageSize,
                       TotalItems = category == null ?
                           repository.Products.Count() :
                           repository.Products.Where(e => e.Category == category).Count()
                   };
                model.CurrentCategory = category;

            }
            else
            {
                model.Products = repository.Products.OrderBy(p => p.ProductID)
                 .Where(p => (category == null || p.Category == category) && (p.CategoryID == id))
                 .Skip((page - 1) * PageSize)
                 .Take(PageSize);
                model.PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = id == null ?
                        repository.Products.Count() :
                        repository.Products.Where(e => e.CategoryID == id).Count()
                };
                model.CurrentCategory = category;
            }
           

           // return View(repository.Products.OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize));
            return View(model);
        }

        public ViewResult ProductDetail(int id, string product, string description, decimal price)
        {
            SportsStore.Domain.Entities.Product products = new SportsStore.Domain.Entities.Product();
            products.ProductID = id;
            products.Name = product;
            products.Description = description;
            products.Price = price;

            return View(products);
        }

        public ViewResult Search(string query, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products.OrderBy(p => p.ProductID)
                .Where(p => p.Name.StartsWith(query))
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = query == null ?
                        repository.Products.Count() :
                        repository.Products.Where(e => e.Category == query).Count()
                },
                CurrentCategory = query
            };
            // return View(repository.Products.OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize));
            return View(model);
        }
        

    }
}
