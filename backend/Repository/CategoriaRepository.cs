using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstractions.IRepository;
using Domain.Models;
using Infrastructure.Data;

namespace Repository;
public class CategoriaRepository : ICategoriaRepository
{

    private readonly ApplicationDbContext _db;

    public CategoriaRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public bool ActualizarCategoria(Categoria categoria)
    {
        categoria.FechaCreacion = DateTime.Now;
        _db.Categorias.Update(categoria);
        return Guardar();   
    }

    public bool BorrarCategoria(Categoria categoria)
    {
        _db .Categorias.Remove(categoria);
        return Guardar() ;
    }

    public bool CrearCategoria(Categoria categoria)
    {
        categoria.FechaCreacion = DateTime.Now;
        _db.Categorias.Add(categoria);
        return Guardar();
    }

    public bool ExisteCategoria(string nombre)
    {
        bool valor = _db.Categorias.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        return valor;
    }

    public bool ExisteCategoria(int id)
    {
        return _db.Categorias.Any(c => c.Id == id);
    }

    public Categoria GetCategoria(int id)
    {
        return _db.Categorias.FirstOrDefault(c => c.Id == id);
    }

    public ICollection<Categoria> GetCategorias()
    {
        return _db.Categorias.OrderBy(c => c.Nombre).ToList();
    }

    public bool Guardar()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }
}
