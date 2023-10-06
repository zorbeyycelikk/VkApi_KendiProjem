using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validations.cs;

public class CreateCardValidator  : AbstractValidator<CardRequest>
{
    
    public CreateCardValidator()
    { 
        // CardHolder
        RuleFor(x => x.CardHolder).NotEmpty().WithMessage("CardHolder is required.");
        RuleFor(x => x.CardHolder).MinimumLength(5).WithMessage("CardHolder length min value is 5.");

        // CardNumber
        RuleFor(x => x.CardNumber).NotEmpty().WithMessage("CardNumber is required.");
        
        // Cvv
        RuleFor(x => x.Cvv)
            .NotEmpty().WithMessage("CVV is required.")
            .MaximumLength(3).WithMessage("CVV en fazla 3 karakter içermelidir.");
    }
}