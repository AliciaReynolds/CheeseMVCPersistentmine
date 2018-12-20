using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC_persistent.ViewModels;

namespace CheeseMVC_persistent.Models
{
    public class Menu
    {
    public int MenuID { get; set; }
    public string MenuName { get; set; }

    public IList<CheeseMenu> CheeseMenus { get; set; }


        //public AddMenuViewModel Name { get; internal set; }
    }
}
