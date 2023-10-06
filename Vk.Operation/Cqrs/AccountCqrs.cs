using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateAccountCommand(AccountRequest Model) : IRequest<ApiResponse<AccountResponse>>;
public record UpdateAccountCommand(AccountRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteAccountCommand(int Id) : IRequest<ApiResponse>;
public record GetAllAccountQuery() : IRequest<ApiResponse<List<AccountResponse>>>;
public record GetAccountByIdQuery(int Id) : IRequest<ApiResponse<AccountResponse>>;