using ControleGastos.Application.DTOs.Transacoes;
using ControleGastos.Domain.Entities;
using FluentValidation;

namespace ControleGastos.Application.Validators;

/// <summary>
/// Validações de entrada (formato)
/// </summary>
public class CriarTransacaoRequestValidator : AbstractValidator<CriarTransacaoRequest>
{
    public CriarTransacaoRequestValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty()
            .MaximumLength(Transacao.DescricaoMaxLength);

        RuleFor(x => x.Valor)
            .GreaterThan(0);

        RuleFor(x => x.Tipo).IsInEnum();

        RuleFor(x => x.CategoriaId)
            .NotEmpty();

        RuleFor(x => x.PessoaId)
            .NotEmpty();
    }
}
