using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation;
using Vk.Schema;

namespace VkApi.Controllers;

[Route("vk/api/v1/[controller]")]
[ApiController]
public class MoneyTransferController : ControllerBase
{
    private IMediator mediator;

    public MoneyTransferController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpPost("InternalTransfer")]
    public async Task<ApiResponse<MoneyTransferResponse>> Post([FromBody] MoneyTransferRequest request)
    {
        var operation = new CreateMoneyTransferCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet("ByAccountId/{accountId}")]
    public async Task<ApiResponse<List<AccountTransactionResponse>>> Post(int accountId)
    {
        var operation = new GetMoneyTransferByAccountIdQuery(accountId);
        var result = await mediator.Send(operation);
        return result;
    }

}