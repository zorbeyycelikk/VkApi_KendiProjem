using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

public class EftTransactionQueryHandler :
    IRequestHandler<GetAllEftTransactionQuery, ApiResponse<List<EftTransactionResponse>>>,
    IRequestHandler<GetEftTransactionByIdQuery, ApiResponse<EftTransactionResponse>>
{
    private readonly IMapper mapper;
    private readonly VkDbContext dbContext;

    public EftTransactionQueryHandler(IMapper mapper , VkDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
    
    public async Task<ApiResponse<List<EftTransactionResponse>>> Handle(GetAllEftTransactionQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<EftTransaction>()
            .Include((x =>x.Account))//AccountName ve AccountNumber buradan gelecek
            .ToListAsync(cancellationToken);

        List<EftTransactionResponse> mapped = mapper.Map<List<EftTransactionResponse>>(entity);
        return new ApiResponse<List<EftTransactionResponse>>(mapped);
        
    }

    public async Task<ApiResponse<EftTransactionResponse>> Handle(GetEftTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<EftTransaction>()
            .Include((x =>x.Account))//AccountName ve AccountNumber buradan gelecek
            .FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<EftTransactionResponse>("Record not found!");
        }
        EftTransactionResponse mapped = mapper.Map<EftTransactionResponse>(entity);
        return new ApiResponse<EftTransactionResponse>(mapped);    }
}