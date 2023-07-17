using AutoMapper;
using Domain.Models;
using API.Dtos;

namespace API.Mappers;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<Categoria, CategoriaDto>().ReverseMap();
        CreateMap<Categoria, CrearCategoriaDto>().ReverseMap();
        CreateMap<Articulo, ArticuloDto>().ReverseMap();
        CreateMap<Usuario, UsuarioDto>().ReverseMap();
        CreateMap<Usuario, UsuarioRegistroDto>().ReverseMap();


    }
}