namespace ControleGastos.Domain.Enums;

/// <summary>
/// Define com quais tipos de transação a categoria pode ser utilizada.
/// <see cref="Ambas"/> Permite despesas e receitas
/// </summary>
public enum FinalidadeCategoria
{
    Despesa = 0,
    Receita = 1,
    Ambas = 2
}
