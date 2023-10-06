using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

public class CardQueryHandler :
    IRequestHandler<GetAllCardQuery, ApiResponse<List<CardResponse>>>,
    IRequestHandler<GetCardByIdQuery, ApiResponse<CardResponse>>
{
    private readonly IMapper mapper;
    private readonly VkDbContext dbContext;

    public CardQueryHandler(IMapper mapper , VkDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
    
    public async Task<ApiResponse<List<CardResponse>>> Handle(GetAllCardQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Card>()
            .Include((x =>x.Account))//AccountName ve AccountNumber buradan gelecek
            .ToListAsync(cancellationToken);

        List<CardResponse> mapped = mapper.Map<List<CardResponse>>(entity);
        return new ApiResponse<List<CardResponse>>(mapped);
        
    }

    public async Task<ApiResponse<CardResponse>> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Card>()
            .Include((x =>x.Account))//AccountName ve AccountNumber buradan gelecek
            .FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<CardResponse>("Record not found!");
        }
        CardResponse mapped = mapper.Map<CardResponse>(entity);
        return new ApiResponse<CardResponse>(mapped);    }
}