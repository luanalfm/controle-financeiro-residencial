using ControleGastos.Application.DTOs.Categorias;
using ControleGastos.Domain.Entities;
using FluentValidation;

namespace ControleGastos.Application.Validators;

/// <summary>
/// Validações de entrada (formato)
/// </summary>
public class CriarCategoriaRequestValidator : AbstractValidator<CriarCategoriaRequest>
{
    public CriarCategoriaRequestValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty()
            .MaximumLength(Categoria.DescricaoMaxLength);

        RuleFor(x => x.Finalidade)
            .IsInEnum();
    }
}
