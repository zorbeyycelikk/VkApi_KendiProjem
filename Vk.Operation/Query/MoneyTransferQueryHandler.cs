using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Schema;

namespace Vk.Operation;

public class MoneyTransferQueryHandler :
    IRequestHandler<GetMoneyTransferByReferenceQuery, ApiResponse<List<AccountTransactionResponse>>>,
    IRequestHandler<GetMoneyTransferByAccountIdQuery, ApiResponse<List<AccountTransactionResponse>>>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public MoneyTransferQueryHandler(VkDbContext dbContext, IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }
    
    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetMoneyTransferByReferenceQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<AccountTransaction>().Include(x => x.Account).ThenInclude(x => x.Customer)
            .Where(x => x.ReferenceNumber == request.ReferenceNumber).ToListAsync(cancellationToken);

        var mapped = mapper.Map<List<AccountTransactionResponse>>(list);
        return new ApiResponse<List<AccountTransactionResponse>>(mapped);
    }

    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetMoneyTransferByAccountIdQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<AccountTransaction>().Include(x => x.Account).ThenInclude(x => x.Customer)
            .Where(x => x.AccountId == request.AccountId).ToListAsync(cancellationToken);

        var mapped = mapper.Map<List<AccountTransactionResponse>>(list);
        return new ApiResponse<List<AccountTransactionResponse>>(mapped);
    }
    
}