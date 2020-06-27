using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/// <summary>
/// Este es el controlador de los FAQs
/// 
/// </summary>
namespace LaCafeteria.Controllers
{
    public class FAQsController
    {

        public List<string> GetCategories()
        {
            FAQsModel fAQsModel = new FAQsModel();
            FAQsHandler fAQsHandler = new FAQsHandler();
            List<string> categories = fAQsHandler.GetCategories(fAQsModel,fAQsHandler);
            return categories;
        }
        public List<string> GetQuestions(string categoria)
        {
            FAQsModel fAQsModel = new FAQsModel();
            FAQsHandler fAQsHandler = new FAQsHandler();
            List<string> questions = fAQsHandler.GetQuestions(fAQsModel,fAQsHandler,categoria);
            return questions;
        }
        public List<string> GetAnswers(string categoria)
        {
            FAQsModel fAQsModel = new FAQsModel();
            FAQsHandler fAQsHandler = new FAQsHandler();
            List<string> answers = fAQsHandler.GetAnswers(fAQsModel,fAQsHandler,categoria);
            return answers;
        }
        public int Borrar(string categoria, string pregunta, string respuesta)
        {
            int error = 0;
            FAQsModel fAQsModel = new FAQsModel();
            FAQsHandler fAQsHandler = new FAQsHandler();
            error = fAQsHandler.Borrar(fAQsModel, fAQsHandler,categoria, pregunta, respuesta);
            return error;
        }
        public int Agregar(string categoria, string pregunta, string respuesta)
        {
            int error = 0;
            FAQsModel fAQsModel = new FAQsModel();
            FAQsHandler fAQsHandler = new FAQsHandler();
            error = fAQsHandler.Agregar(fAQsModel, fAQsHandler,categoria, pregunta, respuesta);
            return error;
        }



    }
}