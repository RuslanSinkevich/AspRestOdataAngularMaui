using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using webapi.DataAccess.Repository.IRepository;
using webapi.Models;


namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITasksRepository _tasks;

    public TasksController(ITasksRepository tasks)
    {
        _tasks = tasks;
    }


    [HttpGet]
    [EnableQuery]
    public async Task<IEnumerable<Tasks>> Get()
    {
        return await Task.FromResult(_tasks.GetAll());
    }


    [HttpGet("{id}")]
    [EnableQuery]
    public IActionResult Get(int key)
    {
        var tasksList = _tasks.FirstOfDefault(p => p.Id == key);

        return Ok(tasksList);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Tasks tasks)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _tasks.Add(tasks);
        _tasks.Save();
        return Created("Tasks", tasks);
    }

    [HttpPut]
    public IActionResult Put([FromODataUri] int key, [FromBody] Tasks tasks)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (key != tasks.Id)
        {
            return BadRequest();
        }
        _tasks.Update(tasks);
        _tasks.Save();
        return NoContent();
    }

    [HttpDelete]
    public IActionResult Delete([FromODataUri] int key)
    {
        var tasks = _tasks.FirstOfDefault(u => u.Id == key);
        if (tasks.Id == 0)
        {
            return NotFound();
        }
        _tasks.Remove(tasks);
        _tasks.Save();
        return NoContent();
    }


}
