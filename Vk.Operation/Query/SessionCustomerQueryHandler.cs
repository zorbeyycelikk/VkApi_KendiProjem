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
    IRequestHandler<GetSessionAddressByIdQuery, ApiResponse<List<AddressResponse>>>,
    IRequestHandler<GetSessionAccountByIdQuery, ApiResponse<List<AccountResponse>>>,
    IRequestHandler<GetSessionAccountTransactionByIdQuery, ApiResponse<List<AccountTransactionResponse>>>,
    IRequestHandler<GetSessionEftTransactionByIdQuery, ApiResponse<List<EftTransactionResponse>>>,
    IRequestHandler<GetSessionCardByIdQuery, ApiResponse<List<CardResponse>>>
{
    private readonly IMapper mapper;
    private readonly VkDbContext dbContext;

    public SessionCustomerQueryHandler(IMapper mapper , VkDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
    
    // Session Customer Info
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
    
    // Session Customer Address Info
    public async Task<ApiResponse<List<AddressResponse>>> Handle(GetSessionAddressByIdQuery request, CancellationToken cancellationToken)
    {
        List<Address> addresses = await dbContext.Set<Address>()
            .Include((x =>x.Customer))
            .Where(x => x.CustomerId == request.Id)
            .ToListAsync(cancellationToken);
        if (addresses == null)
        {
            return new ApiResponse<List<AddressResponse>>("Record not found!");
        }
        List<AddressResponse> mapped = mapper.Map<List<AddressResponse>>(addresses);
        return new ApiResponse<List<AddressResponse>>(mapped);  
    }
    
    // Session Customer Account Info
    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetSessionAccountByIdQuery request, CancellationToken cancellationToken)
    {
        List<Account> entities = await dbContext.Set<Account>()
            .Include((x =>x.Customer))
            .Where(x => x.CustomerId == request.Id)
            .ToListAsync(cancellationToken);
        if (entities == null)
        {
            return new ApiResponse<List<AccountResponse>>("Record not found!");
        }
        List<AccountResponse> mapped = mapper.Map<List<AccountResponse>>(entities);
        return new ApiResponse<List<AccountResponse>>(mapped);  
    }
    
    // Session Customer Account Transaction Info
    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(
        GetSessionAccountTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        List<AccountTransaction> entities = await dbContext.Set<AccountTransaction>()
            .Include((x =>x.Account.Customer))
            .Where(x => x.Account.Customer.Id == request.Id)
            .ToListAsync(cancellationToken);
        
        if (entities == null)
        {
            return new ApiResponse<List<AccountTransactionResponse>>("Record not found!");
        }
        List<AccountTransactionResponse> mapped = mapper.Map<List<AccountTransactionResponse>>(entities);
        return new ApiResponse<List<AccountTransactionResponse>>(mapped);  
    }
    
    // Session Customer Eft Transaction Info
    public async Task<ApiResponse<List<EftTransactionResponse>>> Handle(
        GetSessionEftTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        List<EftTransaction> entities = await dbContext.Set<EftTransaction>()
            .Include((x =>x.Account.Customer))
            .Where(x => x.Account.Customer.Id == request.Id)
            .ToListAsync(cancellationToken);
        
        if (entities == null)
        {
            return new ApiResponse<List<EftTransactionResponse>>("Record not found!");
        }
        List<EftTransactionResponse> mapped = mapper.Map<List<EftTransactionResponse>>(entities);
        return new ApiResponse<List<EftTransactionResponse>>(mapped); 
    }
    
    // Session Customer Eft Transaction Info
    public async Task<ApiResponse<List<CardResponse>>> Handle(
        GetSessionCardByIdQuery request, CancellationToken cancellationToken)
    {
        List<Card> entities = await dbContext.Set<Card>()
            .Include((x =>x.Account.Customer))
            .Where(x => x.Account.Customer.Id == request.Id)
            .ToListAsync(cancellationToken);
        
        if (entities == null)
        {
            return new ApiResponse<List<CardResponse>>("Record not found!");
        }
        List<CardResponse> mapped = mapper.Map<List<CardResponse>>(entities);
        return new ApiResponse<List<CardResponse>>(mapped); 
    }
}