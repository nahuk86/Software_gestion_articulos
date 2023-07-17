using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class UsuarioLoginDto
{
    [Required(ErrorMessage = "El nombre de usuario es requerido")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "El password es requerido")]
    public string Password { get; set; }


}
