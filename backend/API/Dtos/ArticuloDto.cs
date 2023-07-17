using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Dtos;

public class ArticuloDto
{
    public int ArticuloId { get; set; }


    [Required(ErrorMessage = "El titulo es requerido")]
    public string Titulo { get; set; }


    [Required(ErrorMessage = "La descripcion es requerida")]
    public string Descripcion { get; set; }


    public DateTime FechaCreacion { get; set; }

    [ForeignKey("CategoriaId")]
    public int CategoriaId { get; set; }

}
