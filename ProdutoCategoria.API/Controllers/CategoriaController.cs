using Microsoft.AspNetCore.Mvc;
using ProdutoCategoria.Application.DTOs;
using ProdutoCategoria.Application.Services;

namespace ProdutoCategoria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly CategoriaService _service;
    public CategoriasController(CategoriaService service) => _service = service;

    [HttpGet] public async Task<ActionResult<IReadOnlyList<CategoriaDto>>> GetAll() => Ok(await _service.ObterTodosAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoriaDto>> Get(Guid id)
    {
        var dto = await _service.ObterPorIdAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CriarCategoriaDto dto)
    {
        var id = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] AtualizarCategoriaDto dto)
    {
        await _service.AtualizarAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeletarAsync(id);
        return NoContent();
    }
}