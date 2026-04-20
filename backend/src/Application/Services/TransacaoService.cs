using AutoMapper;
using ControleGastos.Application.Abstractions;
using ControleGastos.Application.DTOs.Transacoes;
using ControleGastos.Application.Exceptions;
using ControleGastos.Domain.Entities;

namespace ControleGastos.Application.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public TransacaoService(
        ITransacaoRepository transacaoRepository,
        IPessoaRepository pessoaRepository,
        ICategoriaRepository categoriaRepository,
        IMapper mapper)
    {
        _transacaoRepository = transacaoRepository;
        _pessoaRepository = pessoaRepository;
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Recebemos a requisição, obtemos a pessoa e a categoria, criamos a transação(tudo isso pelo repositório)
    /// Rretornamos a transação criada e mapeada pelo mapper
    /// </summary>
    public async Task<TransacaoResponse> CriarTransacaoAsync(CriarTransacaoRequest request, CancellationToken cancellationToken = default)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(request.PessoaId, cancellationToken)
            ?? throw new NotFoundException("Pessoa não encontrada.");

        var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId, cancellationToken)
            ?? throw new NotFoundException("Categoria não encontrada.");

        var transacao = Transacao.Criar(
            request.Descricao,
            request.Valor,
            request.Tipo,
            pessoa,
            categoria);

        await _transacaoRepository.AdicionarAsync(transacao, cancellationToken);

        return _mapper.Map<TransacaoResponse>(transacao);
    }
    /// <summary>
    /// Chamamos o repositório para listar as transações pelo o id da pessoa e o id da categoria
    /// </summary>
    public async Task<IReadOnlyList<TransacaoResponse>> ListarTransacoesAsync(
        Guid? pessoaId,
        Guid? categoriaId,
        CancellationToken cancellationToken = default)
    {
        var lista = await _transacaoRepository.ListarAsync(pessoaId, categoriaId, cancellationToken);
        return _mapper.Map<IReadOnlyList<TransacaoResponse>>(lista);
    }
}
