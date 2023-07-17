using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;


namespace Abstractions.IRepository;
public interface IUsuarioRepository
{
    Task<Usuario> AuthenticateAsync(string username, string password);

}
