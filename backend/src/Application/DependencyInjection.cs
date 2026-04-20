using System.Reflection;
using ControleGastos.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ControleGastos.Application;

/// <summary>
/// Classe criada para aplicar o conceito de Inversão de Controle, sendo um registro para que interface X entregue a instância do serviço Y
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IPessoaService, PessoaService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<ITransacaoService, TransacaoService>();
        services.AddScoped<IConsultaService, ConsultaService>();

        return services;
    }
}
