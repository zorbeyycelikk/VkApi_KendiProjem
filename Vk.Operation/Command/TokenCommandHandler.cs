 using System.Security.Claims;
 using MediatR;
using Microsoft.EntityFrameworkCore;
 using Vk.Base;
 using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Schema;

namespace Vk.Operation.Command;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>

{
    private readonly VkDbContext dbContext;

    public TokenCommandHandler(VkDbContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Customer>()
            .FirstOrDefaultAsync(x => x.CustomerNumber == request.Model.CustomerNumber, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<TokenResponse>("Invalid user informations");
        }
        
        var md5 = Md5.Create(request.Model.Password.ToUpper());
        if (entity.Password != md5)
        {
            entity.LastActivityDate = DateTime.UtcNow;
            entity.PasswordRetryCount++;
            await dbContext.SaveChangesAsync(cancellationToken);

            return new ApiResponse<TokenResponse>("Invalid user informations");
        }
        
        if (!entity.IsActive)
        {
            return new ApiResponse<TokenResponse>("Invalid user!");
        }
        
        return new ApiResponse<TokenResponse>("Succes user!");
    }
    
    private Claim[] GetClaims(Customer customer)
    {
        var claims = new[]
        {
            new Claim("Id", customer.Id.ToString()),
            new Claim("CustomerNumber", customer.CustomerNumber.ToString()),
            new Claim("Role", customer.Role),
            new Claim("Email", customer.Email),
            new Claim(ClaimTypes.Role, customer.Role),
            new Claim("FullName", $"{customer.FirstName} {customer.LastName}")
        };

        return claims;
    }
}