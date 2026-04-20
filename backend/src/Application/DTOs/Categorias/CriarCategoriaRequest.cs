using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.DTOs.Categorias;

public class CriarCategoriaRequest
{
    public string Descricao { get; set; } = string.Empty;
    public FinalidadeCategoria Finalidade { get; set; }
}
