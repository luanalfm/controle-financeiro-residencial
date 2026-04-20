namespace ControleGastos.Application.DTOs.Consultas;

public class TotaisPorPessoaResponse
{
    public IReadOnlyList<TotaisPorPessoaItem> PorPessoa { get; set; } = Array.Empty<TotaisPorPessoaItem>();
    public TotaisGerais TotalGeral { get; set; } = null!;
}

public class TotaisPorPessoaItem
{
    public Guid PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
}

public class TotaisGerais
{
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal SaldoLiquido { get; set; }
}
