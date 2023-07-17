using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class CrearCategoriaDto
{
    [Required (ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(70, ErrorMessage = "El nombre no puede tener mas de 70 caracteres")]
    public string Nombre { get; set; }
}
