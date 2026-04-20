using ControleGastos.Application.DTOs.Pessoas;
using ControleGastos.Domain.Entities;
using FluentValidation;

namespace ControleGastos.Application.Validators;

/// <summary>
/// Validações de entrada (formato)
/// </summary>
public class CriarPessoaRequestValidator : AbstractValidator<CriarPessoaRequest>
{
    public CriarPessoaRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .MaximumLength(Pessoa.NomeMaxLength);

        RuleFor(x => x.Idade)
            .GreaterThanOrEqualTo(0);
    }
}
