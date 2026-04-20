using ControleGastos.Application.DTOs.Pessoas;

namespace ControleGastos.Application.Services;

public interface IPessoaService
{
    Task<PessoaResponse> CriarPessoaAsync(CriarPessoaRequest request, CancellationToken cancellationToken = default);
    Task<PessoaResponse> AtualizarPessoaAsync(Guid id, AtualizarPessoaRequest request, CancellationToken cancellationToken = default);
    Task DeletarPessoaAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PessoaResponse>> ListarPessoasAsync(CancellationToken cancellationToken = default);
}
