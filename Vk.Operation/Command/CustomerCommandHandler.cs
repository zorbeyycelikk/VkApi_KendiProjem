using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class CustomerCommandHandler :
    IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>,
    IRequestHandler<DeleteCustomerCommand, ApiResponse >,
    IRequestHandler<UpdateCustomerCommand, ApiResponse >
{
    private readonly IMapper mapper;
    private readonly VkDbContext dbContext;
    
    public CustomerCommandHandler(IMapper mapper , VkDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer mapped = mapper.Map<Customer>(request.Model);
        var entity = await dbContext.Set<Customer>().AddAsync(mapped,cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<CustomerResponse>(entity.Entity); // MAPPED OLMAZ MI ?
        return new ApiResponse<CustomerResponse>(response);

    }

    public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.FirstName = request.Model.FirstName;
        entity.LastName = request.Model.LastName;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();

    }
}