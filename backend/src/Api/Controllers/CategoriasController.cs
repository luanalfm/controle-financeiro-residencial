using ControleGastos.Application.DTOs.Categorias;
using ControleGastos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }
    /// <summary>
    /// Lista todas as categorias cadastradas
    /// </summary>
    /// <returns>Lista de categorias</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CategoriaResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CategoriaResponse>>> Listar(CancellationToken cancellationToken)
    {
        var result = await _categoriaService.ListarCategoriasAsync(cancellationToken);
        return Ok(result);
    }
    /// <summary>
    /// Cria uma nova categoria no sistema
    /// </summary>
    /// <param name="request">Dados da categoria a ser criada</param>
    /// <param name="cancellationToken">Token para cancelamento da operação</param>
    /// <returns>Dados da categoria criada com status 201 "Created" e location no header</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CategoriaResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<CategoriaResponse>> Criar([FromBody] CriarCategoriaRequest request, CancellationToken cancellationToken)
    {
        var criado = await _categoriaService.CriarCategoriaAsync(request, cancellationToken);
        return Created($"/api/categorias/{criado.Id}", criado);
    }
}
