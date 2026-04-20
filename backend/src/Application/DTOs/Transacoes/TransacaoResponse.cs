using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.DTOs.Transacoes;

public class TransacaoResponse
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public Guid CategoriaId { get; set; }
    public Guid PessoaId { get; set; }
}
