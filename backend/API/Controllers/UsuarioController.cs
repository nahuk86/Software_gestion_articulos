using Abstractions.IRepository;
using Domain.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        this.usuarioRepository = usuarioRepository;
    }

    public async Task<IActionResult> Login(string username, string password)
    {
        // Authenticate the user
        var usuario = await usuarioRepository.AuthenticateAsync(username, password);

        // If the user is authenticated, return an access token
        if (usuario != null)
        {
            var token = await GenerateAccessTokenAsync(usuario);
            return Ok(new { access_token = token });
        }

        // If the user is not authenticated, return an error message
        return BadRequest("Invalid username or password");
    }

    private async Task<string> GenerateAccessTokenAsync(Usuario usuario)
    {
        // Generate an access token for the user
        var token = Encoding.UTF8.GetBytes(usuario.Username + usuario.Password);

        // Return the access token
        return Convert.ToBase64String(token);
    }
}