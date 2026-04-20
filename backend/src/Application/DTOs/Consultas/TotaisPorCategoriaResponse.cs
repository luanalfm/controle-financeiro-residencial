namespace ControleGastos.Application.DTOs.Consultas;

public class TotaisPorCategoriaResponse
{
    public IReadOnlyList<TotaisPorCategoriaItem> PorCategoria { get; set; } = Array.Empty<TotaisPorCategoriaItem>();
    public TotaisGerais TotalGeral { get; set; } = null!;
}

public class TotaisPorCategoriaItem
{
    public string DescricaoCategoria { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
}
