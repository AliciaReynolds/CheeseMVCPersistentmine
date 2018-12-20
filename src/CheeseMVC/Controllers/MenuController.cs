using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC_persistent.Models;
using CheeseMVC_persistent.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC_persistent.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();
            //not sure if this is right
            return View(menus);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        //is this model binding?
        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    MenuName = addMenuViewModel.MenuName
                };

                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.MenuID);

            }

            return View(addMenuViewModel);
        }

        [HttpGet]
        public IActionResult ViewMenu(int id)
        {
            List<CheeseMenu> items = context
                             .CheeseMenus
                             .Include(item => item.Cheese)
                             .Where(cm => cm.MenuID == id)
                             .ToList();
            Menu theMenu = context.Menus.Single(m => m.MenuID == id);

            ViewMenuViewModel viewModel = new ViewMenuViewModel
            {
                Menu = theMenu,
                Items = items
            };


            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddItem(int id)
        {
            Menu themenu = context.Menus.Single(m => m.MenuID == id);
            List<Cheese> cheeses = context.Cheeses.ToList();

            return View(new AddMenuItemViewModel(themenu, cheeses));
        }

        [HttpPost]
        public IActionResult AddItem (AddMenuItemViewModel addMenuItemViewModel)
        {
            {
                if (ModelState.IsValid)
                {
                    var cheeseID = addMenuItemViewModel.CheeseID;
                    var menuID = addMenuItemViewModel.MenuID;

                    IList<CheeseMenu> existingItems = context.CheeseMenus
                        .Where(cm => cm.CheeseID == cheeseID)
                        .Where(cm => cm.MenuID == menuID).ToList();

                    if (existingItems.Count == 0)
                    {
                        CheeseMenu menuItem = new CheeseMenu
                        {
                            Cheese = context.Cheeses.Single(c => c.ID == cheeseID),
                            Menu = context.Menus.Single(m => m.MenuID == menuID)
                        };

                        context.CheeseMenus.Add(menuItem);
                        context.SaveChanges();
                    }

                return Redirect("/Menu/ViewMenu/" + addMenuItemViewModel.MenuID);

                }

                return View(addMenuItemViewModel);
            }

        }

        //public IActionResult AnchorTagHelper(int id)
        //{
        //    var menu = new Menu
        //    {
        //        Menu = id
        //    };

        //    return View(menu);
        //}
    }
}
