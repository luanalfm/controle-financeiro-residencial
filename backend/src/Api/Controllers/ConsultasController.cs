using ControleGastos.Application.DTOs.Consultas;
using ControleGastos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/consultas")]
public class ConsultasController : ControllerBase
{
    private readonly IConsultaService _consultaService;

    public ConsultasController(IConsultaService consultaService)
    {
        _consultaService = consultaService;
    }

    /// <summary>
    /// Consulta de totais por pessoa:
    /// Lista todas as pessoas cadastradas, exibindo o total de receitas, despesas e o saldo (receita - despesa) de cada uma
    /// Ao final da listagem, exibe o total geral de todas as pessoas incluindo o total de receitas, total de despesas e o saldo líquido
    /// </summary>
    [HttpGet("totais-por-pessoa")]
    [ProducesResponseType(typeof(TotaisPorPessoaResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<TotaisPorPessoaResponse>> TotaisPorPessoa(CancellationToken cancellationToken)
    {
        var result = await _consultaService.ConsultarTotaisPorPessoaAsync(cancellationToken);
        return Ok(result);
    }
    /// <summary>
    /// Consulta de totais por categoria:
    /// Lista todas as categorias cadastradas, exibindo o total de receitas, despesas e o saldo (receita - despesa) de cada uma
    /// Ao final da listagem, exibe o total geral de todas as categorias incluindo o total de receitas, total de despesas e o saldo líquido
    /// </summary>
    [HttpGet("totais-por-categoria")]
    [ProducesResponseType(typeof(TotaisPorCategoriaResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<TotaisPorCategoriaResponse>> TotaisPorCategoria(CancellationToken cancellationToken)
    {
        var result = await _consultaService.ConsultarTotaisPorCategoriaAsync(cancellationToken);
        return Ok(result);
    }
}
