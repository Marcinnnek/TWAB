using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using TWAB.Application.Common.Persistence.Base;
using TWAB.Infrastructure.Persistence.Persistence;

namespace TWAB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRepository<User> _repository;

    public UsersController(IRepository<User> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _repository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
