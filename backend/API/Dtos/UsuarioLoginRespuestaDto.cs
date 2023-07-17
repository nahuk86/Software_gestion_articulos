using Domain.Models;

namespace API.Dtos;

public class UsuarioLoginRespuestaDto
{
    public Usuario Usuario { get; set; }
    public string Token { get; set; }


}
