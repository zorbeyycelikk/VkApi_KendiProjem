using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class EftTransactionCommandHandler:
    IRequestHandler<CreateEftTransactionCommand, ApiResponse<EftTransactionResponse>>,
    IRequestHandler<DeleteEftTransactionCommand, ApiResponse >,
    IRequestHandler<UpdateEftTransactionCommand, ApiResponse >
{
    private readonly IMapper mapper;
    private readonly VkDbContext dbContext;
    
    public EftTransactionCommandHandler(IMapper mapper , VkDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<EftTransactionResponse>> Handle(CreateEftTransactionCommand request, CancellationToken cancellationToken)
    {
        EftTransaction mapped = mapper.Map<EftTransaction>(request.Model);
        var entity = await dbContext.Set<EftTransaction>().AddAsync(mapped,cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<EftTransactionResponse>(entity.Entity); // MAPPED OLMAZ MI ?
        return new ApiResponse<EftTransactionResponse>(response);

    }

    public async Task<ApiResponse> Handle(DeleteEftTransactionCommand request, CancellationToken cancellationToken)
    {
        EftTransaction entity = await dbContext.Set<EftTransaction>().FirstOrDefaultAsync(x => x.Id == request.Id);
        
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }
        
        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateEftTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<EftTransaction>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.Description = request.Model.Description;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}