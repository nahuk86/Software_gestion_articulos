using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

// Describimos los metodos que va a tener nuestro repositorio

namespace Abstractions.IRepository;
public interface ICategoriaRepository
{
    ICollection<Domain.Models.Categoria> GetCategorias();
    Domain.Models.Categoria GetCategoria(int id);

    bool ExisteCategoria(string nombre);
    bool ExisteCategoria(int id);
    bool CrearCategoria(Domain.Models.Categoria categoria);
    bool ActualizarCategoria(Domain.Models.Categoria categoria);
    bool BorrarCategoria(Domain.Models.Categoria categoria);

    bool Guardar();

}
