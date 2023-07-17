using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class CategoriaDto
{
    public int Id { get; set; }

    [Required (ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(70, ErrorMessage = "El nombre no puede tener mas de 70 caracteres")]
    public string Nombre { get; set; }
}
