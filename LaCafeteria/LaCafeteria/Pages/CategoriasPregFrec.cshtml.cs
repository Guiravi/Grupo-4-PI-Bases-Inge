using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class ShowCategoriesFaqsModel : PageModel
    {
        public List<String> categorias { get; set; }
        public ShowCategoriesFaqsModel()
        {
            FAQsController fAQsController = new FAQsController();
            categorias = fAQsController.GetCategories();
        }
        public void OnGet()
        {
           
        }




    }



}