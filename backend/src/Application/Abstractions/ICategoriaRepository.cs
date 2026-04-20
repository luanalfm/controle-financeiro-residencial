using ControleGastos.Domain.Entities;

namespace ControleGastos.Application.Abstractions;

public interface ICategoriaRepository
{
    Task<Categoria?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Categoria>> ListarAsync(CancellationToken cancellationToken = default);
    Task AdicionarAsync(Categoria categoria, CancellationToken cancellationToken = default);
}
