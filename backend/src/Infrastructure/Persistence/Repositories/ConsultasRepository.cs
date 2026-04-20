using ControleGastos.Application.Abstractions;
using ControleGastos.Application.DTOs.Consultas;
using ControleGastos.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Persistence.Repositories;

public class ConsultasRepository : IConsultasRepository
{
    private readonly AppDbContext _context;

    public ConsultasRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém os totais de receitas e despesas por pessoa, calcula o saldo de cada uma
    /// e retorna também os totais gerais com o saldo líquido consolidado.
    /// </summary>
    public async Task<TotaisPorPessoaResponse> ObterTotaisPorPessoaAsync(CancellationToken cancellationToken = default)
    {
        //aqui nós usamos o entity framework para fazer um join entre as tabelas de pessoas e transações e somamos as receitas e despesas de cada pessoa
        //em algumas aplicações considera-se usar o dapper para esse tipo de caso, para uma melhor performance
        var porPessoa = await (
            from p in _context.Pessoas.AsNoTracking()
            join t in _context.Transacoes.AsNoTracking() on p.Id equals t.PessoaId into gt
            select new TotaisPorPessoaItem
            {
                PessoaId = p.Id,
                Nome = p.Nome,
                TotalReceitas = gt.Where(x => x.Tipo == TipoTransacao.Receita).Sum(x => (decimal?)x.Valor) ?? 0,
                TotalDespesas = gt.Where(x => x.Tipo == TipoTransacao.Despesa).Sum(x => (decimal?)x.Valor) ?? 0
            }).ToListAsync(cancellationToken);
        //fazendo o loop para calcular o saldo de cada pessoa
        foreach (var item in porPessoa)
            item.Saldo = item.TotalReceitas - item.TotalDespesas;

        var totalReceitas = await _context.Transacoes.AsNoTracking()
            .Where(x => x.Tipo == TipoTransacao.Receita)
            .SumAsync(x => x.Valor, cancellationToken);

        var totalDespesas = await _context.Transacoes.AsNoTracking()
            .Where(x => x.Tipo == TipoTransacao.Despesa)
            .SumAsync(x => x.Valor, cancellationToken);

        return new TotaisPorPessoaResponse
        {
            PorPessoa = porPessoa,
            TotalGeral = new TotaisGerais
            {
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                SaldoLiquido = totalReceitas - totalDespesas
            }
        };
    }
    /// <summary>
    /// Obtém os totais de receitas e despesas por categoria, calcula o saldo de cada uma
    /// e retorna também os totais gerais com o saldo líquido consolidado.
    /// </summary>
    public async Task<TotaisPorCategoriaResponse> ObterTotaisPorCategoriaAsync(CancellationToken cancellationToken = default)
    {
        var porCategoria = await (
            from c in _context.Categorias.AsNoTracking()
            join t in _context.Transacoes.AsNoTracking() on c.Id equals t.CategoriaId into gt
            select new TotaisPorCategoriaItem
            {
                DescricaoCategoria = c.Descricao,
                TotalReceitas = gt.Where(x => x.Tipo == TipoTransacao.Receita).Sum(x => (decimal?)x.Valor) ?? 0,
                TotalDespesas = gt.Where(x => x.Tipo == TipoTransacao.Despesa).Sum(x => (decimal?)x.Valor) ?? 0
            }).ToListAsync(cancellationToken);

        foreach (var item in porCategoria)
            item.Saldo = item.TotalReceitas - item.TotalDespesas;

        var totalReceitas = await _context.Transacoes.AsNoTracking()
            .Where(x => x.Tipo == TipoTransacao.Receita)
            .SumAsync(x => x.Valor, cancellationToken);

        var totalDespesas = await _context.Transacoes.AsNoTracking()
            .Where(x => x.Tipo == TipoTransacao.Despesa)
            .SumAsync(x => x.Valor, cancellationToken);

        return new TotaisPorCategoriaResponse
        {
            PorCategoria = porCategoria,
            TotalGeral = new TotaisGerais
            {
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                SaldoLiquido = totalReceitas - totalDespesas
            }
        };
    }
}
