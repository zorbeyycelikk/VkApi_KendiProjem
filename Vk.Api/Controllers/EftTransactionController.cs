using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;

[Route("vk/api/v1/[controller]")]
[ApiController]

public class EftTransactionController : ControllerBase
{
    private IMediator mediator;

    public EftTransactionController(IMediator mediator) 
    {
        this.mediator = mediator;
    }


    [HttpGet]
    public async Task<ApiResponse<List<EftTransactionResponse>>> GetAll()
    {
        var operation = new GetAllEftTransactionQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<EftTransactionResponse>> Get(int id)
    {
        var operation = new GetEftTransactionByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<EftTransactionResponse>> Post01([FromBody] EftTransactionRequest request)
    {
        var operation = new CreateEftTransactionCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpPost("ByMoneyTransfer/")]
    public async Task<ApiResponse<EftTransactionResponse>> Post02([FromBody] EftTransactionRequest request)
    {
        var operation = new CreateEftTransaction(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] EftTransactionRequest request)
    {
        var operation = new UpdateEftTransactionCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteEftTransactionCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}