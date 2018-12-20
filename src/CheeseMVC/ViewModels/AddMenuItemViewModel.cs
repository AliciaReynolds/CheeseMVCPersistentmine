using CheeseMVC.Models;
using CheeseMVC_persistent.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC_persistent.ViewModels
{
    public class AddMenuItemViewModel
    {
    public int CheeseID { get; set; }
    public int MenuID { get; set; }

    public Menu Menu { get; set; }
    public List<SelectListItem> Cheeses { get; set; }
    
    public AddMenuItemViewModel () { }
    public AddMenuItemViewModel (Menu menu, IEnumerable<Cheese> cheeses)
        {
        
            Cheeses = new List<SelectListItem>();

            foreach (var cheese in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = cheese.ID.ToString(),
                    Text = cheese.Name
                });
            }
            Menu = menu;
            if (menu != null) { MenuID = menu.MenuID; }
        }
   

}
}
