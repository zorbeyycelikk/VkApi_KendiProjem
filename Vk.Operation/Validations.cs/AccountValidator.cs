using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validations.cs;

public class CreateAccountValidator : AbstractValidator<AccountRequest>
{
    public CreateAccountValidator()
    {
        // Name
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        
        // AccountNumber
        RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("AccountNumber is required.");

        // IBAN
        RuleFor(x => x.IBAN).NotEmpty().WithMessage("IBAN is required.");

        // CurrencyCode
        RuleFor(x => x.CurrencyCode).NotEmpty().WithMessage("CurrencyCode is required.");
    }
}