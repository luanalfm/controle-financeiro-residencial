using ControleGastos.Domain.Enums;
using ControleGastos.Domain.Exceptions;

namespace ControleGastos.Domain.Entities;

/// <summary>
/// Transacao, é uma classe criada para representar a tabela "Transacoes" no banco de dados.
/// Ela é vinculada a uma pessoa e categoria, seu "Valor" é sempre positivo e o "Tipo" define se é entrada ou saída.
/// </summary>
public class Transacao
{
    public const int DescricaoMaxLength = 400;

    /// <summary>
    /// Atribuindo os campos que existirão na tabela Transacoes, o set está como "private", pois é uma boa prática limitarmos esse acesso direto as propriedades
    /// </summary>
    public Guid Id { get; private set; }
    public string Descricao { get; private set; } = null!;
    public decimal Valor { get; private set; }
    public TipoTransacao Tipo { get; private set; }
    public Guid CategoriaId { get; private set; }
    public Guid PessoaId { get; private set; }

    /// <summary>
    /// Chamamos um construtor privado para o Entity poder mapear as propriedades para a tabela
    /// </summary>
    private Transacao()
    {
    }

    /// <summary>
    /// Esse método cria uma transação aplicando todas as invariantes de domínio (valor, categoria, idade da pessoa).
    /// </summary>
    public static Transacao Criar(
        string descricao,
        decimal valor,
        TipoTransacao tipo,
        Pessoa pessoa,
        Categoria categoria)
    {
        ValidarInvariantes(descricao, valor, tipo, pessoa, categoria);

        return new Transacao
        {
            Id = Guid.NewGuid(),
            Descricao = descricao.Trim(),
            Valor = valor,
            Tipo = tipo,
            CategoriaId = categoria.Id,
            PessoaId = pessoa.Id
        };
    }
    /// <summary>
    /// Esse método Valida tudo
    /// </summary>
    private static void ValidarInvariantes(
        string descricao,
        decimal valor,
        TipoTransacao tipo,
        Pessoa pessoa,
        Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("A Descrição da transação é obrigatória.");

        if (descricao.Length > DescricaoMaxLength)
            throw new DomainException($"A Descrição não pode exceder {DescricaoMaxLength} caracteres.");

        if (valor <= 0)
            throw new DomainException("O Valor deve ser maior que zero.");

        if (!categoria.EhCompativelCom(tipo))
            throw new DomainException(
                "A categoria não é compatível com o tipo da transação.");

        if (tipo == TipoTransacao.Receita && !pessoa.PodeTerReceita())
            throw new DomainException("Uma Pessoa menor de idade não pode registrar transações do tipo Receita.");
    }
}
