using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

public class SessionCustomerQueryHandler : 
    IRequestHandler<GetSessionCustomerByIdQuery, ApiResponse<CustomerResponse>>,
    IRequestHandler<GetSessionAccountByIdQuery, ApiResponse<AccountResponse>>

{
    private readonly IMapper mapper;
    private readonly VkDbContext dbContext;

    public SessionCustomerQueryHandler(IMapper mapper , VkDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
    
    public async Task<ApiResponse<AccountResponse>> Handle(GetSessionAccountByIdQuery request, CancellationToken cancellationToken)
    {
        Account entity = await dbContext.Set<Account>()
            .Include( x => x.Customer)
            .FirstOrDefaultAsync(x => x.CustomerId == request.Id , cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<AccountResponse>("Record not found!");
        }
        AccountResponse mapped = mapper.Map<AccountResponse>(entity);
        return new ApiResponse<AccountResponse>(mapped);    }
    
    public async Task<ApiResponse<CustomerResponse>> Handle(GetSessionCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        //Customer entity= unitOfWork.CustomerRepository.GetById(request.Id);
        
        Customer entity = await dbContext.Set<Customer>()
                                       .FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<CustomerResponse>("Record not found!");
        }
        
        CustomerResponse mapped = mapper.Map<CustomerResponse>(entity);
        return new ApiResponse<CustomerResponse>(mapped);
        
    }
}