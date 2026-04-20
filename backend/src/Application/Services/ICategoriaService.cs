using ControleGastos.Application.DTOs.Categorias;

namespace ControleGastos.Application.Services;

public interface ICategoriaService
{
    /// <summary>
    /// Chamamos o método do repositório para criar a categoria de forma assíncrona
    /// </summary>
    Task<CategoriaResponse> CriarCategoriaAsync(CriarCategoriaRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Chamamos o método do repositório para listar as categorias de forma assíncrona
    /// </summary>
    Task<IReadOnlyList<CategoriaResponse>> ListarCategoriasAsync(CancellationToken cancellationToken = default);
}
