using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class UsuarioDto
{

    public int Id { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }

    public string Nombre { get; set; }
    public string Apellido { get; set; }


}
