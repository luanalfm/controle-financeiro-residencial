using ControleGastos.Application.DTOs.Transacoes;
using ControleGastos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/transacoes")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacoesController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TransacaoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TransacaoResponse>>> Listar(
        [FromQuery] Guid? pessoaId,
        [FromQuery] Guid? categoriaId,
        CancellationToken cancellationToken)
    {
        var result = await _transacaoService.ListarTransacoesAsync(pessoaId, categoriaId, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TransacaoResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<TransacaoResponse>> Criar([FromBody] CriarTransacaoRequest request, CancellationToken cancellationToken)
    {
        var criado = await _transacaoService.CriarTransacaoAsync(request, cancellationToken);
        return Created($"/api/transacoes/{criado.Id}", criado);
    }
}
