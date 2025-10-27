using Microsoft.AspNetCore.Mvc;
using SoftPlan.TaskManager.Application.DTOs;
using SoftPlan.TaskManager.Application.Services;
using System;
using System.Threading.Tasks;

namespace SoftPlan.TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _service;
    public TasksController(TaskService service) => _service = service;

    [HttpPost]
    [ProducesResponseType(typeof(TaskDto), 201)]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByUser), new { userId = created.UserId }, created);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId) => Ok(await _service.GetByUserAsync(userId));

    [HttpPut("{id:guid}/complete")]
    public async Task CompleteAsync(Guid id) => await _service.CompleteTaskAsync(id);

    [HttpDelete("{id:guid}")]
    public async Task DeleteAsync(Guid id) => await _service.DeleteTaskAsync(id);
}

