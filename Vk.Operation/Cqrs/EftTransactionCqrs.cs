using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateEftTransactionCommand(EftTransactionRequest Model) : IRequest<ApiResponse<EftTransactionResponse>>;
public record UpdateEftTransactionCommand(EftTransactionRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteEftTransactionCommand(int Id) : IRequest<ApiResponse>;
public record GetAllEftTransactionQuery() : IRequest<ApiResponse<List<EftTransactionResponse>>>;
public record GetEftTransactionByIdQuery(int Id) : IRequest<ApiResponse<EftTransactionResponse>>;
public record CreateEftTransaction(EftTransactionRequest Model) : IRequest<ApiResponse<EftTransactionResponse>>;
