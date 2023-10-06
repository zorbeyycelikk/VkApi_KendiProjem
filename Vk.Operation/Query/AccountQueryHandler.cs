using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

public class AccountQueryHandler :
    IRequestHandler<GetAllAccountQuery, ApiResponse<List<AccountResponse>>>,
    IRequestHandler<GetAccountByIdQuery, ApiResponse<AccountResponse>>
{
    private readonly IMapper mapper;
    private readonly VkDbContext dbContext;

    public AccountQueryHandler(IMapper mapper , VkDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
    
    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Account>()
            .Include((x =>x.Customer))//CustomerName buradan gelecek
           // .Include(x => x.AccountTransactions) Bunlarla ilgil iresposne yok o yüzden eklemek opsiyonel
           // .Include(x => x.EftTransactions)
            .ToListAsync(cancellationToken);

        List<AccountResponse> mapped = mapper.Map<List<AccountResponse>>(entity);
        return new ApiResponse<List<AccountResponse>>(mapped);
        
    }

    public async Task<ApiResponse<AccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        Account entity = await dbContext.Set<Account>()
            .Include( x => x.Customer)
            .FirstOrDefaultAsync(x => x.AccountNumber == request.Id , cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<AccountResponse>("Record not found!");
        }
        AccountResponse mapped = mapper.Map<AccountResponse>(entity);
        return new ApiResponse<AccountResponse>(mapped);    }
}