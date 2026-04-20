using ControleGastos.Application.Abstractions;
using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Persistence.Repositories;

/// <summary>
/// Consulta, inserção e remoção na entidade Pessoa. Com operações CRUD, validação de dependências e listagem ordenada.
/// </summary>
public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _context;

    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pessoa?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Pessoas.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Pessoa>> ListarAsync(CancellationToken cancellationToken = default)
        => await _context.Pessoas.AsNoTracking().OrderBy(x => x.Nome).ToListAsync(cancellationToken);

    public async Task AdicionarAsync(Pessoa pessoa, CancellationToken cancellationToken = default)
    {
        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Pessoa pessoa, CancellationToken cancellationToken = default)
    {
        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SalvarAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public Task<bool> ExisteReceitaParaPessoaAsync(Guid pessoaId, CancellationToken cancellationToken = default)
        => _context.Transacoes.AsNoTracking()
            .AnyAsync(t => t.PessoaId == pessoaId && t.Tipo == TipoTransacao.Receita, cancellationToken);
}
