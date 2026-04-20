using ControleGastos.Application.DTOs.Consultas;

namespace ControleGastos.Application.Services;

public interface IConsultaService
{
    /// <summary>
    /// Chamamos o método do repositório para obter os totais por pessoa de forma assíncrona
    /// </summary>
    Task<TotaisPorPessoaResponse> ConsultarTotaisPorPessoaAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Chamamos o método do repositório para obter os totais por categoria de forma assíncrona
    /// </summary>
    Task<TotaisPorCategoriaResponse> ConsultarTotaisPorCategoriaAsync(CancellationToken cancellationToken = default);
}
