using API.Dtos;
using AutoMapper;
using API.Mappers;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Abstractions.IRepository;

namespace API.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _ctRepo;
    private readonly IMapper _mapper;

    public CategoriasController(ICategoriaRepository ctRepo, IMapper mapper)
    {
        _ctRepo = ctRepo;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<CategoriaDto>))]
    [ProducesResponseType(400)]


    public IActionResult GetCategoria()
    {
        var listaCategorias = _ctRepo.GetCategorias();
        var listaCategoriasDTO = new List<CategoriaDto>();
        foreach (var lista in listaCategorias)
        {
            listaCategoriasDTO.Add(_mapper.Map<CategoriaDto>(lista));
        }
        return Ok(listaCategoriasDTO);
    }

    //

    [HttpGet("(categoriaId:int)", Name = "GetCategoria")]
    [ProducesResponseType(200, Type = typeof(CategoriaDto))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]

    public IActionResult GetCategoria(int categoriaId)
    {
        var itemCategoria = _ctRepo.GetCategoria(categoriaId);
        if (itemCategoria == null)
        {
            return NotFound();
        }
        var itemCategoriaDTO = _mapper.Map<CategoriaDto>(itemCategoria);
        return Ok(itemCategoriaDTO);
    }



    // / <param name="crearCategoriaDTO"></param>
    // / <returns></returns>

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(CategoriaDto))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]

    public IActionResult CrearCategoria([FromBody] CrearCategoriaDto crearCategoriaDTO)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (crearCategoriaDTO == null)
        {
            return BadRequest(ModelState);
        }
        if (_ctRepo.ExisteCategoria(crearCategoriaDTO.Nombre))
        {
            ModelState.AddModelError("", "La categoria ya existe");
            return StatusCode(404, ModelState);
        }

        var categoria = _mapper.Map<Categoria>(crearCategoriaDTO);
        if (!_ctRepo.CrearCategoria(categoria))
        {
            ModelState.AddModelError("", $"Algo salio mal guardando el registro {categoria.Nombre}");
            return StatusCode(500, ModelState);
        }

        return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria);

    }


    [HttpPatch("{categoriaId:int}", Name = "ActualizarPatchCategoria")]
    [ProducesResponseType(201, Type = typeof(CategoriaDto))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]

    public IActionResult ActualizarPatchCategoria(int categoriaId, [FromBody] CategoriaDto categoriaDTO)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (categoriaDTO == null || categoriaId != categoriaDTO.Id)
        {
            return BadRequest(ModelState);
        }

        var categoria = _mapper.Map<Categoria>(categoriaDTO);
        if (!_ctRepo.ActualizarCategoria(categoria))
        {
            ModelState.AddModelError("", $"Algo salio mal al actualizar el registro {categoria.Nombre}");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }


    [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
    [ProducesResponseType(204)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]

    public IActionResult BorrarCategoria(int categoriaId)
    {
        if (!_ctRepo.ExisteCategoria(categoriaId))
        {
            return NotFound();
        }
        var categoria = _ctRepo.GetCategoria(categoriaId);
        if (!_ctRepo.BorrarCategoria(categoria))
        {
            ModelState.AddModelError("", $"Algo salio mal al borrar el registro {categoria.Nombre}");
            return StatusCode(500, ModelState);
        }
        return NoContent();
    }


}

