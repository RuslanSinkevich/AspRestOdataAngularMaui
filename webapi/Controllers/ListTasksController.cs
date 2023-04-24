using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using webapi.DataAccess.Repository.IRepository;
using webapi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ListTasksController : ControllerBase
{
    private readonly IListTasksRepository _listTasks;

    public ListTasksController(IListTasksRepository listTasks)
    {
        _listTasks = listTasks;
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<TasksL> Get()
    {
        return _listTasks.GetAll();
    }

    [HttpGet("{id}")]
    [EnableQuery]
    public IActionResult Get(int key)
    {
        var tasksList = _listTasks.FirstOfDefault(p => p.Id == key);

        return Ok(tasksList);
    }

    [HttpPost]
    public IActionResult Post([FromBody] TasksL tasksList)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _listTasks.Add(tasksList);
        _listTasks.Save();
        return Created("ListTasks", tasksList);
    }

    [HttpPut]
    public IActionResult Put([FromODataUri] int key, [FromBody] TasksL tasksList)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (key != tasksList.Id)
        {
            return BadRequest();
        }
        _listTasks.Update(tasksList);
        _listTasks.Save();
        return NoContent();
    }

    [HttpDelete]
    public IActionResult Delete([FromODataUri] int key)
    {
        var tasksList = _listTasks.FirstOfDefault(u => u.Id == key);
        if (tasksList.Id == 0)
        {
            return NotFound();
        }
        _listTasks.Remove(tasksList);
        _listTasks.Save();
        return NoContent();
    }
}
