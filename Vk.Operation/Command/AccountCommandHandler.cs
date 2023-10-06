using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class AccountCommandHandler:
    IRequestHandler<CreateAccountCommand, ApiResponse<AccountResponse>>,
    IRequestHandler<DeleteAccountCommand, ApiResponse >,
    IRequestHandler<UpdateAccountCommand, ApiResponse >
{
    private readonly IMapper mapper;
    private readonly VkDbContext dbContext;
    
    public AccountCommandHandler(IMapper mapper , VkDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        Account mapped = mapper.Map<Account>(request.Model);
        var entity = await dbContext.Set<Account>().AddAsync(mapped,cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<AccountResponse>(entity.Entity); // MAPPED OLMAZ MI ?
        return new ApiResponse<AccountResponse>(response);    
    }

    public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        Account entity = await dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();    }

    public async Task<ApiResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        Account entity = await dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.Name= request.Model.Name;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}