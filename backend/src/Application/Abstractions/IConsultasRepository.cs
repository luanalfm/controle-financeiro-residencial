using ControleGastos.Application.DTOs.Consultas;

namespace ControleGastos.Application.Abstractions;

public interface IConsultasRepository
{
    /// <summary>
    /// Consulta de totais por pessoa
    /// </summary>
    Task<TotaisPorPessoaResponse> ObterTotaisPorPessoaAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Consulta de totais por categoria:
    /// </summary>
    Task<TotaisPorCategoriaResponse> ObterTotaisPorCategoriaAsync(CancellationToken cancellationToken = default);
}
