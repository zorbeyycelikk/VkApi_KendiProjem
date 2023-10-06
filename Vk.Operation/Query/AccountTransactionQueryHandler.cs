using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

public class AccountTransactionQueryHandler :
    IRequestHandler<GetAllAccountTransactionQuery, ApiResponse<List<AccountTransactionResponse>>>,
    IRequestHandler<GetAccountTransactionByIdQuery, ApiResponse<AccountTransactionResponse>>
{
    private readonly IMapper mapper;
    private readonly VkDbContext dbContext;

    public AccountTransactionQueryHandler(IMapper mapper , VkDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
    
    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetAllAccountTransactionQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<AccountTransaction>()
            .Include((x =>x.Account))//AccountName,AccountNumber,CustomerName , CustomerNumber buradan gelecek
            .ToListAsync(cancellationToken);

        List<AccountTransactionResponse> mapped = mapper.Map<List<AccountTransactionResponse>>(entity);
        return new ApiResponse<List<AccountTransactionResponse>>(mapped);
        
    }

    public async Task<ApiResponse<AccountTransactionResponse>> Handle(GetAccountTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<AccountTransaction>()
            .Include((x =>x.Account))//AccountName,AccountNumber,CustomerName , CustomerNumber buradan gelecek
            .FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<AccountTransactionResponse>("Record not found!");
        }
        AccountTransactionResponse mapped = mapper.Map<AccountTransactionResponse>(entity);
        return new ApiResponse<AccountTransactionResponse>(mapped);    }
}