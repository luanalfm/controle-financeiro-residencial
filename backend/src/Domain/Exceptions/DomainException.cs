namespace ControleGastos.Domain.Exceptions;

/// <summary>
/// Para gerar exceções customizadas das regras de negócio do domínio.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}
