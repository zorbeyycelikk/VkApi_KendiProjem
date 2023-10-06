using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validations.cs;

public class CreateAddressValidator  : AbstractValidator<AddressRequest>
{
    public CreateAddressValidator()
    { 
        // Address Line 1
        RuleFor(x => x.AddressLine1).NotEmpty().WithMessage("AddressLine1 is required.");
        RuleFor(x => x.AddressLine1).MinimumLength(5).WithMessage("AddressLine1 length min value is 5.");

        // Address Line 2
        RuleFor(x => x.AddressLine2).NotEmpty().WithMessage("AddressLine2 is required.");
        
        // City
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
    
        // County
        RuleFor(x => x.County).NotEmpty().WithMessage("County is required.");
    }
}