using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;


[Route("vk/api/v1/[controller]")]
[ApiController]
public class CustomerServiceController: ControllerBase
{
    private IMediator mediator;

    public CustomerServiceController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("GetCustomerInfo")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<CustomerResponse>> GetSessionCustomerInfo()
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet("GetCustomerAddress")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<AddressResponse>>> GetSessionCustomerAddressInfo()
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionAddressByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet("GetCustomerAccounts")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<AccountResponse>>> GetSessionAccounts()
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionAccountByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet("GetCustomerAccountTransactions")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<AccountTransactionResponse>>> GetSessionAccountTransactions()
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionAccountTransactionByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet("GetCustomerEftTransactions")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<EftTransactionResponse>>> GetSessionEftTransactions()
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionEftTransactionByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet("GetCustomerCard")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<CardResponse>>> GetSessionCard()
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCardByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }

      
    

}