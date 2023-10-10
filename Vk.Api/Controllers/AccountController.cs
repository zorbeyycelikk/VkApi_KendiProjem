using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;

[Route("vk/api/v1/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private IMediator mediator;

    public AccountController(IMediator mediator) 
    {
        this.mediator = mediator;
    }


    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<AccountResponse>>> GetAll()
    {
        var operation = new GetAllAccountQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<AccountResponse>> Get(int id)
    {
        var operation = new GetAccountByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<AccountResponse>> Post([FromBody] AccountRequest request)
    {
        var operation = new CreateAccountCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] AccountRequest request)
    {
        var operation = new UpdateAccountCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteAccountCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}