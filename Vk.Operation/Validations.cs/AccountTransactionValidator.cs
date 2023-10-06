using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validations.cs;

public class CreateAccountTransactionValidator : AbstractValidator<AccountTransactionRequest>
{

    public CreateAccountTransactionValidator()
    {
        // Description
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.")
            .MaximumLength(100).WithMessage("Description en fazla 100 karakter içermelidir.");
    }
}