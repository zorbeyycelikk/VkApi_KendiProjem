using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record GetSessionCustomerByIdQuery(int Id) : IRequest<ApiResponse<CustomerResponse>>;

public record GetSessionAccountByIdQuery(int Id) : IRequest<ApiResponse<AccountResponse>>;