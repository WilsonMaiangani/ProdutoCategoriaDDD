using Microsoft.AspNetCore.Mvc;
using ProdutoCategoria.Application.DTOs;
using ProdutoCategoria.Application.Services;

namespace ProdutoCategoria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _service;
    public ProdutosController(ProdutoService service) => _service = service;

    [HttpGet] public async Task<ActionResult<IReadOnlyList<ProdutoDto>>> GetAll() => Ok(await _service.ObterTodosAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProdutoDto>> Get(Guid id)
    {
        var dto = await _service.ObterPorIdAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet("{id:guid}/com-categoria")]
    public async Task<ActionResult<ProdutoComCategoriaDto>> GetComCategoria(Guid id)
    {
        var dto = await _service.ObterComCategoriaAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CriarProdutoDto dto)
    {
        var id = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] AtualizarProdutoDto dto)
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