using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;

[Route("vk/api/v1/[controller]")]
[ApiController]

public class CardController : ControllerBase
{
    private IMediator mediator;

    public CardController(IMediator mediator) 
    {
        this.mediator = mediator;
    }


    [HttpGet]
    public async Task<ApiResponse<List<CardResponse>>> GetAll()
    {
        var operation = new GetAllCardQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<CardResponse>> Get(int id)
    {
        var operation = new GetCardByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<CardResponse>> Post([FromBody] CardRequest request)
    {
        var operation = new CreateCardCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] CardRequest request)
    {
        var operation = new UpdateCardCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteCardCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}