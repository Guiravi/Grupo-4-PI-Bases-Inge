
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models
{
    public class MiembroSolicitaSubirRangoNucleoModel
    {
        public string usernameNucleoFK { get; set; }

        public string usernameMiembroFK { get; set; }

        public string estado { get; set; }
    }
}
