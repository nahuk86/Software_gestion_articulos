using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class UsuarioRegistroDto
{
    [Required(ErrorMessage = "El nombre de usuario es requerido")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "El password es requerido")]
    public string Password { get; set; }

    public string Role { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El apellido es requerido")]
    public string Apellido { get; set; }

}
