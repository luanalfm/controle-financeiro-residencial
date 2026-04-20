using ControleGastos.Application.DTOs.Pessoas;
using ControleGastos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/pessoas")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoasController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }
    /// <summary>
    /// Lista todas as pessoas cadastradas
    /// </summary>
    /// <returns>Lista de pessoas com seus respectivos dados</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PessoaResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PessoaResponse>>> Listar(CancellationToken cancellationToken)
    {
        var result = await _pessoaService.ListarPessoasAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Cria uma nova pessoa no sistema.
    /// </summary>
    /// <param name="request">Dados da pessoa a ser criada</param>
    /// <param name="cancellationToken">Token para cancelamento da operação</param>
    /// <returns>Dados da pessoa criada com status 201 Created e location no header</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PessoaResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<PessoaResponse>> Criar([FromBody] CriarPessoaRequest request, CancellationToken cancellationToken)
    {
        var criado = await _pessoaService.CriarPessoaAsync(request, cancellationToken);
        return Created($"/api/pessoas/{criado.Id}", criado);
    }
    /// <summary>
    /// Atualiza os dados de uma pessoa existente.
    /// </summary>
    /// <param name="id">Identificador único da pessoa</param>
    /// <param name="request">Dados atualizados da pessoa</param>
    /// <param name="cancellationToken">Token para cancelamento da operação</param>
    /// <returns>Dados da pessoa atualizada</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PessoaResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<PessoaResponse>> Atualizar(
        [FromRoute] Guid id,
        [FromBody] AtualizarPessoaRequest request,
        CancellationToken cancellationToken)
    {
        var atualizado = await _pessoaService.AtualizarPessoaAsync(id, request, cancellationToken);
        return Ok(atualizado);
    }
    /// <summary>
    /// Remove uma pessoa
    /// </summary>
    /// <param name="id">Identificador único</param>
    /// <param name="cancellationToken">Token para cancelamento da operação</param>
    /// <returns>Retorna Status 204 "No Content" em caso de sucesso</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Deletar([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _pessoaService.DeletarPessoaAsync(id, cancellationToken);
        return NoContent();
    }
}
