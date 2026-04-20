using ControleGastos.Domain.Entities;

namespace ControleGastos.Application.Abstractions;

public interface IPessoaRepository
{
    Task<Pessoa?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Pessoa>> ListarAsync(CancellationToken cancellationToken = default);
    Task AdicionarAsync(Pessoa pessoa, CancellationToken cancellationToken = default);
    Task RemoverAsync(Pessoa pessoa, CancellationToken cancellationToken = default);
    Task SalvarAsync(CancellationToken cancellationToken = default);
    Task<bool> ExisteReceitaParaPessoaAsync(Guid pessoaId, CancellationToken cancellationToken = default);
}
