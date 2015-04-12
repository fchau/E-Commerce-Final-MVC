using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
  
        private IProductsRepository repository;
        private ICategoryRepository catRepository;

        //Constructor
        public NavController(IProductsRepository repo, ICategoryRepository cat)
        {
            repository = repo;
            catRepository = cat;
        }

        //Navigation for Sidebar, referenced by "Model" in the Sidebar.cshtml page.
        public PartialViewResult Sidebar(string category)
        {
            CategoryListModel model = new CategoryListModel
            {

                Categories = catRepository.Categories
                .Where(c => category == c.ParentName)

            };

         

            return PartialView(model);
        }

        //Navigation for TopBar
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }

    }
}
