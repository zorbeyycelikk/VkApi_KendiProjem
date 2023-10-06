using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validations.cs;

public class CreateCustomerValidator : AbstractValidator<CustomerRequest>
{

    public CreateCustomerValidator()
    {
        // First Name
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Firstname is required.");
        RuleFor(x => x.FirstName).MinimumLength(20).WithMessage("Firstname length min value is 20.");
        
        // Last Name
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
        RuleFor(x => x.LastName).MinimumLength(20).WithMessage("LastName length min value is 20.");

        // Email
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(x => x.Email).MinimumLength(20).WithMessage("Email length min value is 20.");
        
        // Password
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password boş olamaz.")
            .MinimumLength(12).WithMessage("Password en az 12 karakterden oluşmalıdır.")
            .Matches("[A-Z]").WithMessage("Password en az bir büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("Password en az bir küçük harf içermelidir.")
            .Matches("[0-9]").WithMessage("Password en az bir rakam içermelidir.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password en az bir özel karakter içermelidir.");

        // Customer Number
        RuleFor(x => x.CustomerNumber)
            .NotEmpty().WithMessage("CustomerNumber boş olamaz.")
            .GreaterThan(0).WithMessage("CustomerNumber 0'dan büyük olmalıdır.");

        // Role
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role boş olamaz.");
    }
}