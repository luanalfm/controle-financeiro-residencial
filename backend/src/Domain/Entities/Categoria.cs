using ControleGastos.Domain.Enums;

namespace ControleGastos.Domain.Entities;

/// <summary>
/// Categoria, é uma classe criada para representar a tabela "Categorias" no banco de dados.
/// </summary>
public class Categoria
{
    public const int DescricaoMaxLength = 400;

    /// <summary>
    /// Atribuindo os campos que existirão na tabela Categoria, o set está como "private", pois é uma boa prática limitarmos esse acesso direto as propriedades
    /// </summary>
    public Guid Id { get; private set; }
    public string Descricao { get; private set; } = null!;
    public FinalidadeCategoria Finalidade { get; private set; } //esse campo utiliza um enum para termos um valor pré-definido 

    /// <summary>
    /// Chamamos um construtor privado para o Entity poder mapear as propriedades para a tabela
    /// </summary>
    private Categoria()
    {
    }
    /// <summary>
    /// Construtor utilizado para definir o valor único do id e chamar a validação dos outros dados
    /// </summary>
    public Categoria(string descricao, FinalidadeCategoria finalidade)
    {
        Id = Guid.NewGuid();
        DefinirDados(descricao, finalidade);
    }

    /// <summary>
    /// O método "EhCompativelCom" aplica a regra:
    ///  restringir a utilização de categorias conforme o valor definido no campo finalidade. 
    ///  Ex: se o tipo da transação é despesa, não poderá utilizar uma categoria que tenha a finalidade receita.
    /// </summary>
    public bool EhCompativelCom(TipoTransacao tipoTransacao)
    {
        return Finalidade switch
        {
            FinalidadeCategoria.Despesa => tipoTransacao == TipoTransacao.Despesa,
            FinalidadeCategoria.Receita => tipoTransacao == TipoTransacao.Receita,
            FinalidadeCategoria.Ambas => true,
            _ => false
        };
    }

    /// <summary>
    /// Define os dados da categoria e chama os métodos de validação
    /// </summary>
    private void DefinirDados(string descricao, FinalidadeCategoria finalidade)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new Exceptions.DomainException("A Descrição da categoria é obrigatória.");

        if (descricao.Length > DescricaoMaxLength)
            throw new Exceptions.DomainException($"A Descrição não pode exceder {DescricaoMaxLength} caracteres.");

        Descricao = descricao.Trim();
        Finalidade = finalidade;
    }
}
