using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

// Describimos los metodos que va a tener nuestro repositorio

namespace Abstractions.IRepository;
public interface IArticuloRepository
{
    ICollection<Articulo> GetArticulos();
    Articulo GetArticulo(int id);

    bool ExisteArticulo(string nombre);
    bool ExisteArticulo(int id);
    bool CrearArticulo(Articulo articulo);
    bool ActualizarArticulo(Articulo articulo);
    bool BorrarArticulo(Articulo articulo);

    //Metodos para buscar articulos por categoria y por nombre

    ICollection<Articulo> GetArticulosPorCategoria(int categoriaId);
    ICollection<Articulo> BuscarArticulos(string titulo);


    bool Guardar();

}
