using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

public class CustomerQueryHandler : 
    IRequestHandler<GetAllCustomerQuery, ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerByIdQuery, ApiResponse<CustomerResponse>>
{
    private readonly IMapper mapper;
    private readonly VkDbContext dbContext;

    public CustomerQueryHandler(IMapper mapper , VkDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
        
    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        //List<Customer> entities = unitOfWork.CustomerRepository.GetAll("Address" , "Account");

        
        // Bütün kullanıcı değerleri veri tabanından çekiliyor
        List<Customer> entities = await dbContext.Set<Customer>()
            .Include(x => x.Accounts)
            .Include(x => x.Addresses)
            .ToListAsync(cancellationToken);
    
        // bütün customer'ların olduğu listeyi customerResponse'ta bulunanlar ile eşleştirip onu geri döndürüyoruz
        List<CustomerResponse> mapped = mapper.Map<List<CustomerResponse>>(entities);
        
        //CustomerResponse'a cast edildi ve ApiResponse iskeletinde çıktı veriliyor.
        return new ApiResponse<List<CustomerResponse>>(mapped);
        
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        //Customer entity= unitOfWork.CustomerRepository.GetById(request.Id);
        
        Customer entity = await dbContext.Set<Customer>()
                                       .Include( x => x.Accounts)
                                       .Include( x => x.Addresses)
                                       .FirstOrDefaultAsync(x => x.CustomerNumber == request.Id , cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<CustomerResponse>("Record not found!");
        }
        
        CustomerResponse mapped = mapper.Map<CustomerResponse>(entity);
        return new ApiResponse<CustomerResponse>(mapped);
        
    }
}