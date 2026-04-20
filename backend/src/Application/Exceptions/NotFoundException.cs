namespace ControleGastos.Application.Exceptions;

/// <summary>
/// Recurso solicitado não existe. Mapeada para 404 pelo middleware global.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}
