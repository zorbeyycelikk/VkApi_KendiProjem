using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

public class AddressQueryHandler :
    IRequestHandler<GetAllAddressQuery, ApiResponse<List<AddressResponse>>>,
    IRequestHandler<GetAddressByIdQuery, ApiResponse<AddressResponse>>
{
private readonly IMapper mapper;
private readonly VkDbContext dbContext;

public AddressQueryHandler(IMapper mapper , VkDbContext dbContext)
{
    this.mapper = mapper;
    this.dbContext = dbContext;
}

public async Task<ApiResponse<List<AddressResponse>>> Handle(GetAllAddressQuery request, CancellationToken cancellationToken)
{
    List<Address> entities = await dbContext.Set<Address>()
                            .Include(x => x.Customer)
                            .ToListAsync();
    List<AddressResponse> mapped = mapper.Map<List<AddressResponse>>(entities);
    return new ApiResponse<List<AddressResponse>>(mapped);
}

public async Task<ApiResponse<AddressResponse>> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
{
    Address entity = await dbContext.Set<Address>()
        .Include(x => x.Customer)
        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    if (entity == null)
    {
        return new ApiResponse<AddressResponse>("Record not found!");
    }

    AddressResponse mapped = mapper.Map<AddressResponse>(entity);
    return new ApiResponse<AddressResponse>(mapped);

}
}