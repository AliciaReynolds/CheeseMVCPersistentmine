using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Data;
using System.Collections.Generic;
using CheeseMVC.Models;
using System.Linq;
using CheeseMVC.ViewModels;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            List<CheeseCategory> cat = context.Categories.ToList();

            return View(cat);
        }

        public IActionResult Add()
        {
            AddCategoryViewModel addcat = new AddCategoryViewModel();
            return View(addcat);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addcat)
        {
            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing cheeses
                CheeseCategory newCat = new CheeseCategory
                {
                    Name = addcat.Name,
                };


                context.Categories.Add(newCat);
                context.SaveChanges();

                return Redirect("/Category");
            }

            return View(addcat);


        }
    }
}
