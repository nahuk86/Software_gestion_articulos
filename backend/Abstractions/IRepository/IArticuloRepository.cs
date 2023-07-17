using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

// Describimos los metodos que va a tener nuestro repositorio

namespace Abstractions.IRepository;
public interface IArticuloRepository
{
    ICollection<Domain.Models.Articulo> GetArticulos();
    Domain.Models.Articulo GetArticulo(int id);

    bool ExisteArticulo(string nombre);
    bool ExisteArticulo(int id);
    bool CrearArticulo(Domain.Models.Articulo articulo);
    bool ActualizarArticulo(Domain.Models.Articulo articulo);
    bool BorrarArticulo(Domain.Models.Articulo articulo);

    //Metodos para buscar articulos por categoria y por nombre

    ICollection<Domain.Models.Articulo> GetArticulosPorCategoria(int categoriaId);
    ICollection<Domain.Models.Articulo> BuscarArticulos(string titulo);


    bool Guardar();

}
