using ControleGastos.Application.Abstractions;
using ControleGastos.Application.DTOs.Consultas;

namespace ControleGastos.Application.Services;

public class ConsultaService : IConsultaService
{
    private readonly IConsultasRepository _consultasRepository;

    public ConsultaService(IConsultasRepository consultasRepository)
    {
        _consultasRepository = consultasRepository;
    }

    /// <summary>
    /// Chamamos o método do repositório para obter os totais por pessoa de forma assíncrona
    /// </summary>
    public Task<TotaisPorPessoaResponse> ConsultarTotaisPorPessoaAsync(CancellationToken cancellationToken = default)
        => _consultasRepository.ObterTotaisPorPessoaAsync(cancellationToken);

    /// <summary>
    /// Chamamos o método do repositório para obter os totais por categoria de forma assíncrona
    /// </summary>
    public Task<TotaisPorCategoriaResponse> ConsultarTotaisPorCategoriaAsync(CancellationToken cancellationToken = default)
        => _consultasRepository.ObterTotaisPorCategoriaAsync(cancellationToken);
}
