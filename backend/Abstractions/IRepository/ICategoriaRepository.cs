using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

// Describimos los metodos que va a tener nuestro repositorio

namespace Abstractions.IRepository;
public interface ICategoriaRepository
{
    ICollection<Categoria> GetCategorias();
    Categoria GetCategoria(int id);

    bool ExisteCategoria(string nombre);
    bool ExisteCategoria(int id);
    bool CrearCategoria(Categoria categoria);
    bool ActualizarCategoria(Categoria categoria);
    bool BorrarCategoria(Categoria categoria);

    bool Guardar();

}
