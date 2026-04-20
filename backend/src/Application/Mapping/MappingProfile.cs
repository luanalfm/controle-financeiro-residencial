using AutoMapper;
using ControleGastos.Application.DTOs.Categorias;
using ControleGastos.Application.DTOs.Pessoas;
using ControleGastos.Application.DTOs.Transacoes;
using ControleGastos.Domain.Entities;

namespace ControleGastos.Application.Mapping;

/// <summary>
/// Convers�es de formato, para n�o mostrar a entidade como resposta(por seguran�a)
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Pessoa, PessoaResponse>();
        CreateMap<Categoria, CategoriaResponse>();
        CreateMap<Transacao, TransacaoResponse>();
    }
}
