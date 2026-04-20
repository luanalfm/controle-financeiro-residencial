using ControleGastos.Domain.Entities;

namespace ControleGastos.Application.Abstractions;

public interface ITransacaoRepository
{
    Task<IReadOnlyList<Transacao>> ListarAsync(
        Guid? pessoaId,
        Guid? categoriaId,
        CancellationToken cancellationToken = default);

    Task AdicionarAsync(Transacao transacao, CancellationToken cancellationToken = default);
}
