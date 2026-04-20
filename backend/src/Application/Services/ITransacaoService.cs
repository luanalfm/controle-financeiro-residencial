using ControleGastos.Application.DTOs.Transacoes;

namespace ControleGastos.Application.Services;

public interface ITransacaoService
{
    Task<TransacaoResponse> CriarTransacaoAsync(CriarTransacaoRequest request, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TransacaoResponse>> ListarTransacoesAsync(
        Guid? pessoaId,
        Guid? categoriaId,
        CancellationToken cancellationToken = default);
}
