using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validations.cs;

public class CreateEftTransactionValidator : AbstractValidator<EftTransactionRequest>
{
    public CreateEftTransactionValidator()
    { 
        // FromAccountId
        RuleFor(x => x.FromAccountId)
            .NotEmpty().WithMessage("FromAccountId boş olamaz.")
            .GreaterThan(0).WithMessage("FromAccountId 0'dan büyük olmalıdır.");

        // FromAccountId
        RuleFor(x => x.ToAccountId)
            .NotEmpty().WithMessage("ToAccountId boş olamaz.")
            .GreaterThan(0).WithMessage("ToAccountId 0'dan büyük olmalıdır.");
        
        // Amount
        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount boş olamaz.");

        // Description
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.")
            .MaximumLength(100).WithMessage("Description en fazla 100 karakter içermelidir.");
    }


  
}