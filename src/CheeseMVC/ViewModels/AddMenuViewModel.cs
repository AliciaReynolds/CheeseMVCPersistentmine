using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC_persistent.ViewModels
{
    public class AddMenuViewModel
    {
    [Required]
    [Display(Name="Menu Name")]
    public string MenuName { get; set; }

}
}
