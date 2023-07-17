using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstractions.IRepository;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class ArticuloRepository : IArticuloRepository
{

    private readonly ApplicationDbContext _db;

    public ArticuloRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public bool ActualizarArticulo(Articulo articulo)
    {
        articulo.FechaCreacion = DateTime.Now;
        _db.Articulos.Update(articulo);
        return Guardar();   
    }

    public bool BorrarArticulo(Articulo articulo)
    {
        _db.Articulos.Remove(articulo);
        return Guardar() ;
    }

    public ICollection<Articulo> BuscarArticulos(string titulo)
    {
        IQueryable<Articulo> query = _db.Articulos;
        if (!string.IsNullOrEmpty(titulo))
        {
            query = query.Where(c => c.Titulo.Contains(titulo) || c.Descripcion.Contains(titulo));
        }
        return query.ToList();
    }

    public bool CrearArticulo(Articulo articulo)
    {
        articulo.FechaCreacion = DateTime.Now;
        _db.Articulos.Add(articulo);
        return Guardar();
    }

    public bool ExisteArticulo(string titulo)
    {
        bool valor = _db.Articulos.Any(c => c.Titulo.ToLower().Trim() == titulo.ToLower().Trim());
        return valor;
    }

    public bool ExisteArticulo(int id)
    {
        return _db.Articulos.Any(c => c.ArticuloId == id);
    }

    public Articulo GetArticulo(int id)
    {
        return _db.Articulos.FirstOrDefault(c => c.ArticuloId == id);
    }

    public ICollection<Articulo> GetArticulos()
    {
        return _db.Articulos.OrderBy(c => c.Titulo).ToList();
    }

    public ICollection<Articulo> GetArticulosPorCategoria(int categoriaId)
    {
         return _db.Articulos.Include(c => c.Categoria).Where(c => c.CategoriaId == categoriaId).ToList();
        
    }


    public bool Guardar()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }

}
