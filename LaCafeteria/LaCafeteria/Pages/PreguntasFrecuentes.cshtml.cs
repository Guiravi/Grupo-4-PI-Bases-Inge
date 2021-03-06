﻿using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class ShowFaqsModel : PageModel
    {
        public List<String> questions { get; set; }
        public List<String> answers { get; set; }

        [BindProperty(SupportsGet = true)]
        public string categoria { get; set ; }

        public ShowFaqsModel()
        {
            

        }
        public void OnGet()
        {
            FAQsController fAQsController = new FAQsController();
            questions = fAQsController.GetQuestions(this.categoria);
            answers = fAQsController.GetAnswers(this.categoria);
            return;
        }




    }



}