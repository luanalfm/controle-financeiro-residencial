using ControleGastos.Application.Abstractions;
using ControleGastos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Persistence.Repositories;

/// <summary>
///  Obtém a categoria por id, lista as categorias ordenando por descrição, adiciona categorias => Tudo isso chamando o context e usando o entity framework
/// </summary>
public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Categoria>> ListarAsync(CancellationToken cancellationToken = default)
        => await _context.Categorias.AsNoTracking().OrderBy(x => x.Descricao).ToListAsync(cancellationToken);

    public async Task AdicionarAsync(Categoria categoria, CancellationToken cancellationToken = default)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
