using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

// Session Customer Info +
public record GetSessionCustomerByIdQuery(int Id) : IRequest<ApiResponse<CustomerResponse>>;

// Session Customer Address Info +
public record GetSessionAddressByIdQuery(int Id) : IRequest<ApiResponse<List<AddressResponse>>>;

// Session Customer Account Info +
public record GetSessionAccountByIdQuery(int Id) : IRequest<ApiResponse<List<AccountResponse>>>;

// Session Customer Account Transaction Info +
public record GetSessionAccountTransactionByIdQuery(int Id) : IRequest<ApiResponse<List<AccountTransactionResponse>>>;

// Session Customer Eft Transaction Info +
public record GetSessionEftTransactionByIdQuery(int Id) : IRequest<ApiResponse<List<EftTransactionResponse>>>;

// Session Customer Card Info
public record GetSessionCardByIdQuery(int Id) : IRequest<ApiResponse<List<CardResponse>>>;

