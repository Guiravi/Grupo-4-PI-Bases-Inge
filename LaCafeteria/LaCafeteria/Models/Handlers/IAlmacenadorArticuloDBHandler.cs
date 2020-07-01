using System.Collections.Generic;

namespace LaCafeteria.Models.Handlers
{
    public interface IAlmacenadorArticuloDBHandler
    {
        void GuardarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<CategoriaTopicoModel> listaCategoriaTopicos);
    }
}