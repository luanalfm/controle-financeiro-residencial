using AutoMapper;
using ControleGastos.Application.Abstractions;
using ControleGastos.Application.DTOs.Pessoas;
using ControleGastos.Application.Exceptions;
using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Exceptions;

namespace ControleGastos.Application.Services;

/// <summary>
/// Classe de serviço com as funções CRUD
/// </summary>
public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IMapper _mapper;

    public PessoaService(IPessoaRepository pessoaRepository, IMapper mapper)
    {
        _pessoaRepository = pessoaRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Recebemos a requisição, chamamos o construtor da classe, chamamos o repositório para criar no banco essa nova Pessoa
    /// e retornamos o que foi registrado após convertemos os dados no mapper
    /// </summary>
    public async Task<PessoaResponse> CriarPessoaAsync(CriarPessoaRequest request, CancellationToken cancellationToken = default)
    {
        var pessoa = new Pessoa(request.Nome, request.Idade);
        await _pessoaRepository.AdicionarAsync(pessoa, cancellationToken);
        return _mapper.Map<PessoaResponse>(pessoa);
    }

    /// <summary>
    /// Recebemos o ID e a requisição, obtemos a pessoa pelo id dela no repositório, validamos se ela quer atualizar para uma idade menor do que 18
    /// mesmo já havendo receita registrada e atualizamos a pessoa, retornando os novos dados de forma convertida no mapper
    /// </summary>
    public async Task<PessoaResponse> AtualizarPessoaAsync(Guid id, AtualizarPessoaRequest request, CancellationToken cancellationToken = default)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Pessoa não encontrada.");

        if (request.Idade < 18)
        {
            var temReceita = await _pessoaRepository.ExisteReceitaParaPessoaAsync(id, cancellationToken);
            if (temReceita)
                throw new DomainException(
                    "Não é possível definir uma idade menor que 18 anos para uma pessoa que já possui receitas registradas.");
        }

        pessoa.Atualizar(request.Nome, request.Idade);
        await _pessoaRepository.SalvarAsync(cancellationToken);
        return _mapper.Map<PessoaResponse>(pessoa);
    }

    /// <summary>
    /// Recebemos o ID e chamamos o repositório para deleter o registro
    /// </summary>
    public async Task DeletarPessoaAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Pessoa não encontrada.");

        await _pessoaRepository.RemoverAsync(pessoa, cancellationToken);
    }

    /// <summary>
    /// Listamos as pessoas de forma assíncrona e mapeamos o retorno em uma list
    /// </summary>
    public async Task<IReadOnlyList<PessoaResponse>> ListarPessoasAsync(CancellationToken cancellationToken = default)
    {
        var lista = await _pessoaRepository.ListarAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<PessoaResponse>>(lista);
    }
}
