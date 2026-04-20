namespace ControleGastos.Domain.Entities;

/// <summary>
/// Pessoa, é uma classe criada para representar a tabela "Categorias" no banco de dados.
/// </summary>
public class Pessoa
{
    public const int NomeMaxLength = 200;

    /// <summary>
    /// Atribuindo os campos que existirão na tabela Pessoas, o set está como "private", pois é uma boa prática limitarmos esse acesso direto as propriedades
    /// </summary>
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public int Idade { get; private set; } //Menores de 18 anos não podem registrar receitas, apenas despesas.

    /// <summary>
    /// Chamamos um construtor privado para o Entity poder mapear as propriedades para a tabela
    /// </summary>
    private Pessoa()
    {
    }
    /// <summary>
    /// Construtor utilizado para definir o valor único do id e chamar a validação dos outros dados
    /// </summary>
    public Pessoa(string nome, int idade)
    {
        Id = Guid.NewGuid();
        DefinirDados(nome, idade);
    }

    /// <summary>Validação se a pessoa pode ter transações do tipo "Receita"</summary>
    public bool PodeTerReceita() => Idade >= 18;

    /// <summary>Atualiza a entidade e posteriormente os dados da tabela Pessoas</summary>
    public void Atualizar(string nome, int idade)
    {
        DefinirDados(nome, idade);
    }

    /// <summary>
    /// Define os dados de Pessoa e chama os métodos de validação
    /// </summary>
    private void DefinirDados(string nome, int idade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new Exceptions.DomainException("O Nome é obrigatório.");

        if (nome.Length > NomeMaxLength)
            throw new Exceptions.DomainException($"O Nome não pode exceder {NomeMaxLength} caracteres.");

        if (idade < 0)
            throw new Exceptions.DomainException("A Idade não pode ser negativa.");

        Nome = nome.Trim();
        Idade = idade;
    }
}
