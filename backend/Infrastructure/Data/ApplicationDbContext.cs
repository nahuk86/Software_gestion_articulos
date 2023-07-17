using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Models.Categoria> Categorias { get; set; }
    public DbSet<Domain.Models.Articulo> Articulos { get; set; }
    public DbSet<Domain.Models.Usuario> Usuarios { get; set; }


}
