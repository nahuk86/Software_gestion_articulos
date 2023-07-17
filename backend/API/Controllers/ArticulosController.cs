using Abstractions.IRepository;
using API.Dtos;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/articulos")]
[ApiController]

public class ArticulosController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IArticuloRepository _articuloRepository;

    public ArticulosController(IArticuloRepository artRepo, IMapper mapper)
    {
        _articuloRepository = artRepo;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ArticuloDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetArticulos()
    {
        var listaArticulos = _articuloRepository.GetArticulos(); // esta linea es la que se encarga de traer los datos de la base de datos
        var listaArticulosDTO = new List<ArticuloDto>(); // esta linea es la que se encarga de mapear los datos de la base de datos a la clase ArticuloDto
        foreach (var lista in listaArticulos) // este foreach es el que se encarga de mapear los datos de la base de datos a la clase ArticuloDto
        {
            listaArticulosDTO.Add(_mapper.Map<ArticuloDto>(lista)); // esta linea es la que se encarga de mapear los datos de la base de datos a la clase ArticuloDto
        }
        return Ok(listaArticulosDTO); // esta linea es la que se encarga de mapear los datos de la base de datos a la clase ArticuloDto

    }

    [HttpGet("{articuloId:int}", Name = "GetArticulo")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticuloDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetArticulo(int articuloId)
    {
        var itemArticulo = _articuloRepository.GetArticulo(articuloId);
        if (itemArticulo == null)
        {
            return NotFound();
        }
        var itemArticuloDTO = _mapper.Map<ArticuloDto>(itemArticulo);
        return Ok(itemArticuloDTO);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ArticuloDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CrearArticulo([FromBody] ArticuloDto crearArticuloDTO)
    {
        if (crearArticuloDTO == null)
        {
            return BadRequest(ModelState);
        }
        if (_articuloRepository.ExisteArticulo(crearArticuloDTO.Titulo))
        {
            ModelState.AddModelError("", "El articulo ya existe");
            return StatusCode(404, ModelState);
        }
        var articulo = _mapper.Map<Articulo>(crearArticuloDTO);
        if (!_articuloRepository.CrearArticulo(articulo))
        {
            ModelState.AddModelError("", $"Algo salio mal guardando el registro {articulo.Titulo}");
            return StatusCode(500, ModelState);
        }
        return CreatedAtRoute("GetArticulo", new { articuloId = articulo.ArticuloId }, articulo);
    }


    [HttpPatch("{articuloId:int}", Name = "ActualizarPatchArticulo")]
    [ProducesResponseType(201, Type = typeof(ArticuloDto))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]

    public IActionResult ActualizarPatchArticulo(int articuloId, [FromBody] ArticuloDto articuloDTO)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var articulo = _mapper.Map<Articulo>(articuloDTO);
        
        if (!_articuloRepository.ActualizarArticulo(articulo))
        {
            ModelState.AddModelError("", $"Algo salio mal al actualizar el registro {articulo.Titulo}");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }


    [HttpDelete("{articuloId:int}", Name = "BorrarArticulo")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public IActionResult BorrarArticulo(int articuloId)
    {

        if (!_articuloRepository.ExisteArticulo(articuloId))
        {
            return NotFound();
        }

        var articulo = _articuloRepository.GetArticulo(articuloId);

        if (!_articuloRepository.BorrarArticulo(articulo))
        {
            ModelState.AddModelError("", $"Algo salio mal al eliminar el registro {articulo.Titulo}");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpGet("GetArticulosPorCategoria/{categoriaId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public IActionResult GetArticulosPorCategoria(int categoriaId) 
    {
        var listaArticulos = _articuloRepository.GetArticulosPorCategoria(categoriaId);
        if (listaArticulos == null)
        {
            return NotFound();
        }
        var listaArticulosDTO = new List<ArticuloDto>();

        foreach (var lista in listaArticulos)
        {
            listaArticulosDTO.Add(_mapper.Map<ArticuloDto>(lista));
        }
        return Ok(listaArticulosDTO);
 
    }


    [HttpGet("BuscarArticulos")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public IActionResult BuscarArticulos(string titulo)
    {

        try
        {
            var resultado = _articuloRepository.BuscarArticulos(titulo.Trim());
            if (resultado.Any())
            {
                return Ok(resultado);
            }
            return NotFound();

        }
        catch (Exception ex) 
        {
            return StatusCode(500, "Error interno del servidor");
        }


    }

}
