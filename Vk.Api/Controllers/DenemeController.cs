using Microsoft.AspNetCore.Mvc;
using Vk.Data.Domain;
using Vk.Data.Uow;

namespace VkApi.Controllers;

[Route("vk/api/v1/[controller]")]
[ApiController]

public class DenemeController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    
    public DenemeController(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    
    [HttpGet]
    public List<Customer> Get()
    {
        /*
         3'ü de aynı işlemi yapar. 
         dbContext'i tanımlayıp constructor'da tanımlamayı unutma
        var list01 = unitOfWork.CustomerRepository.GetAll();
        var list02 = dbContext.Set<Customer>().AsNoTracking().ToList();
        var list03 = dbContext.Customers.AsNoTracking().ToList();
        */
        
        return unitOfWork.CustomerRepository.GetAll();
    }
    
    [HttpGet("{id}")]
    public Customer Get(int id)
    {
        return unitOfWork.CustomerRepository.GetById(id);
    }

    [HttpPost]
    public void Post([FromBody] Customer request)
    {
        unitOfWork.CustomerRepository.Insert(request);
        unitOfWork.Complete();
    }
    
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Customer request)
    {
        unitOfWork.CustomerRepository.Update(request);
        unitOfWork.Complete();
    }
    
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        unitOfWork.CustomerRepository.Delete(id);
        unitOfWork.Complete();
    }
}