using AutoMapper;
using ControleGastos.Application.Abstractions;
using ControleGastos.Application.DTOs.Categorias;
using ControleGastos.Domain.Entities;

namespace ControleGastos.Application.Services;

/// <summary>
/// Classe de serviço para criar e lista categorias de forma assíncrona
/// </summary>
public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Recebemos a requisição, chamamos o construtor da classe, chamamos o repositório para criar no banco essa nova categoria
    /// e retornamos o que foi registrado após convertemos os dados no mapper
    /// </summary>
    public async Task<CategoriaResponse> CriarCategoriaAsync(CriarCategoriaRequest request, CancellationToken cancellationToken = default)
    {
        var categoria = new Categoria(request.Descricao, request.Finalidade);
        await _categoriaRepository.AdicionarAsync(categoria, cancellationToken);
        return _mapper.Map<CategoriaResponse>(categoria);
    }

    /// <summary>
    /// Chamamos o repositório para listar todas as categorias e retornamos a lista já convertida no mapper
    /// </summary>
    public async Task<IReadOnlyList<CategoriaResponse>> ListarCategoriasAsync(CancellationToken cancellationToken = default)
    {
        var lista = await _categoriaRepository.ListarAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<CategoriaResponse>>(lista);
    }
}
