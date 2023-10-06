using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;

[Route("vk/api/v1/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private IMediator mediator;

    public CustomerController(IMediator mediator) 
    {
        this.mediator = mediator;
    }


    [HttpGet]
    public async Task<ApiResponse<List<CustomerResponse>>> GetAll()
    {
        var operation = new GetAllCustomerQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<CustomerResponse>> Get(int id)
    {
        var operation = new GetCustomerByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<CustomerResponse>> Post([FromBody] CustomerRequest request)
    {
        var operation = new CreateCustomerCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] CustomerRequest request)
    {
        var operation = new UpdateCustomerCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteCustomerCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

}