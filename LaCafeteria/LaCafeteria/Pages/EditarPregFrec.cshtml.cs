﻿using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;

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
           
            this.error = fAQsController.Borrar(categoria, pregunta, respuesta);
            switch (error)
            {
                case 0:
                    AvisosInmediatos.Set(this, "exitoEliminar", "Se ha eliminado la pregunta y la respuesta con éxito en su respectiva categoría", AvisosInmediatos.TipoAviso.Exito);
                    //TempData["Message"] = "Se ha eliminado la pregunta y la respuesta con éxito en su respectiva categoría";
                    break;
                case 1:
                    AvisosInmediatos.Set(this, "camposIncompletos", "Por favor complete todos los campos antes de solicitar una funcionalidad", AvisosInmediatos.TipoAviso.Error);
                    //TempData["Message"] = "Por favor complete todos los campos antes de solicitar una funcionalidad";
                    break;
                case 3:
                    AvisosInmediatos.Set(this, "noEncontrados", "Por favor ingrese otra pregunta y respuesta, ya que su pregunta no se ecuentra para eliminarla", AvisosInmediatos.TipoAviso.Error);
                    //TempData["Message"] = "Por favor ingrese otra pregunta y respuesta, ya que su pregunta no se ecuentra para eliminarla";
                    break;
            }
        }
        public IActionResult OnPostAdd()
        {
            this.error = fAQsController.Agregar(categoria, pregunta, respuesta);
            switch (error) {
                case 0:
                    AvisosInmediatos.Set(this, "exitoAgregar", "Se ha agregado la pregunta y la respuesta con éxito en su respectiva categoría", AvisosInmediatos.TipoAviso.Exito);
                    return Redirect("/CategoriasPregFrec");
                    //TempData["Message"] = "Se ha agregado la pregunta y la respuesta con éxito en su respectiva categoría";
                    break;
                case 1:
                    AvisosInmediatos.Set(this, "camposIncompletos", "Por favor complete todos los campos antes de solicitar una funcionalidad", AvisosInmediatos.TipoAviso.Error);
                    //TempData["Message"] = "Por favor complete todos los campos antes de solicitar una funcionalidad";
                    return Page();
                    break;
                case 2:
                    AvisosInmediatos.Set(this,"camposRepetidos" ,"Por favor ingrese otra pregunta y respuesta, ya que su pregunta está repetida para la categoría seleccionada", AvisosInmediatos.TipoAviso.Error);
                    return Page();
                    //TempData["Message"] = "Por favor ingrese otra pregunta y respuesta, ya que su pregunta está repetida para la categoría seleccionada";
                    break;

            }
            return Page();
        }




    }



}