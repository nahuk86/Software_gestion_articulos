using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Models;
public class Articulo
{
    public int ArticuloId { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }

    [ForeignKey("CategoriaId")]
    public int CategoriaId { get; set; }

    public Categoria Categoria { get; set; }

}
