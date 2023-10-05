using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;
/*
 * IRequest mediatR'den türemektedir.
 * Create , Update , Delete işlemleri 'Command' , Read ise 'Query' olarak geçmektedir.
 * Kullanıcıdan istenen veriler request olarak gelir.' : ' dan sonrası apiresponse mimarisinde customerresponse dönsün isteniyor
 * Bunları query ve command içinde yazmadan önce mapleme işlemi yapılmalı.
 */

public record CreateCustomerCommand(CustomerRequest Model) : IRequest<ApiResponse<CustomerResponse>>;
public record UpdateCustomerCommand(CustomerRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteCustomerCommand(int Id) : IRequest<ApiResponse>;
public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomerByIdQuery(int Id) : IRequest<ApiResponse<CustomerResponse>>;