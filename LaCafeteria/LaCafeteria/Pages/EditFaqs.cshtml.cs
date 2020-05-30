using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class EditFaqsModel : PageModel
    {


        [BindProperty]
        public string categoria { get; set ; }
        [BindProperty]
        public string pregunta { get; set; }
        [BindProperty]
        public string respuesta { get; set; }

        public FAQsController fAQsController;

        public int error;

        public EditFaqsModel()
        {
            fAQsController = new FAQsController();
            error = 0;
        }
        public void OnGet()
        {
           

        }
        public void OnPostDelete()
        {
            //Request.Form["emailaddress"];
            //Request.Form["emailaddress"];
            //Request.Form["emailaddress"];
            this.error = fAQsController.Borrar(categoria, pregunta, respuesta);
            switch (error)
            {
                case 0:
                    TempData["Message"] = "Se ha eliminado la pregunta y la respuesta con éxito en su respectiva categoría";
                    break;
                case 1:
                    TempData["Message"] = "Por favor complete todos los campos antes de solicitar una funcionalidad";
                    break;
                case 3:
                    TempData["Message"] = "Por favor ingrese otra pregunta y respuesta, ya que su pregunta no se ecuentra para eliminarla";
                    break;
                default:
                    TempData["Message"] = "";
                    break;
            }
        }
        public void OnPostAdd()
        {
            this.error = fAQsController.Agregar(categoria, pregunta, respuesta);
            switch (error) {
                case 0:
                    TempData["Message"] = "Se ha agregado la pregunta y la respuesta con éxito en su respectiva categoría";
                    break;
                case 1:
                    TempData["Message"] = "Por favor complete todos los campos antes de solicitar una funcionalidad";
                    break;
                case 2:
                    TempData["Message"] = "Por favor ingrese otra pregunta y respuesta, ya que su pregunta está repetida para la categoría seleccionada";
                    break;
                default:
                    TempData["Message"] = "";
                    break;
            }
        }




    }



}