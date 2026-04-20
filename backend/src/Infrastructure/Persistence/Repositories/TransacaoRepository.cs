using ControleGastos.Application.Abstractions;
using ControleGastos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Persistence.Repositories;

/// <summary>
/// Buscamos as transa��es mais recentes a partir de filtros e adicionamos transa��es
/// </summary>
public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Transacao>> ListarAsync(
        Guid? pessoaId,
        Guid? categoriaId,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Transacoes.AsNoTracking().AsQueryable();

        if (pessoaId.HasValue)
            query = query.Where(x => x.PessoaId == pessoaId.Value);

        if (categoriaId.HasValue)
            query = query.Where(x => x.CategoriaId == categoriaId.Value);

        return await query.OrderByDescending(x => x.Id).ToListAsync(cancellationToken);
    }

    public async Task AdicionarAsync(Transacao transacao, CancellationToken cancellationToken = default)
    {
        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
