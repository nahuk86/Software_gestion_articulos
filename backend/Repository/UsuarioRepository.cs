using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstractions.IRepository;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class UsuarioRepository : IUsuarioRepository
{

    private readonly ApplicationDbContext contexto;

    public UsuarioRepository(ApplicationDbContext contexto)
    {
        this.contexto = contexto;
    }

    public async Task<Usuario> AuthenticateAsync(string username, string password)
    {
        // Authenticate the user
        var usuario = await contexto.Usuarios.Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();

        // If the user is authenticated, return the user
        if (usuario != null)
        {
            return usuario;
        }

        // If the user is not authenticated, return null
        return null;
    }
}