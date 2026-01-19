using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TodoController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            var todo = _context.Todos.Find(id);

            if (todo == null)
                return NotFound(new { message = $"Todo #{id} not found" });

            return Ok(todo);
        }

        [HttpGet("FindAll")]
        public IActionResult FindAll()
        {
            var todos = _context.Todos.ToList();

            if (!todos.Any())
                return NotFound(new { message = "There are no todos yet" });

            return Ok(todos);
        }

        [HttpGet("FindByKeyword")]
        public IActionResult FindByKeyword(string keyword)
        {
            var todos = _context.Todos.Where(t => t.Task.Contains(keyword)).ToList();

            if (!todos.Any())
                return NotFound(new { message = $"No todos were found with the keyword {keyword}" });

            return Ok(todos);
        }

        [HttpGet("FindByDate")]
        public IActionResult FindByDate(DateTime date)
        {
            var todos = _context.Todos.Where(x => x.Deadline == date.Date).ToList();

            if (!todos.Any())
                return NotFound(new { message = $"No todos were found with the date {date:yyyy-MM-dd}" });

            return Ok(todos);
        }

        [HttpGet("FindByStatus")]
        public IActionResult FindByStatus(EnumChoreStatus status)
        {
            var todos = _context.Todos.Where(t => t.Status == status).ToList();

            if (!todos.Any())
                return NotFound(new { message = $"No todos were found with the status {status}" });

            return Ok(todos);
        }

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            if (todo.Deadline == DateTime.MinValue)
                return BadRequest(new { error = "Todo date is required" });

            _context.Todos.Add(todo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(FindById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Todo todo)
        {
            var todoFromDB = _context.Todos.Find(id);

            if (todoFromDB == null)
                return NotFound(new { message = $"Todo #{id} not found" });

            if (todo.Deadline == DateTime.MinValue)
                return BadRequest(new { error = "Todo date is required" });

            todoFromDB.Task = todo.Task;
            todoFromDB.Deadline = todo.Deadline;
            todoFromDB.Status = todo.Status;
            todoFromDB.Description = todo.Description;

            _context.SaveChanges();

            return Ok(todoFromDB);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todoFromDB = _context.Todos.Find(id);

            if (todoFromDB == null)
                return NotFound(new { message = $"Todo #{id} not found" });

            _context.Todos.Remove(todoFromDB);
            _context.SaveChanges();

            return NoContent();
        }
    }
}