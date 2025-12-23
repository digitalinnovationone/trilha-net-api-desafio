using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {

            var tarefa  = _context.Tarefas.Find(id);

            if (tarefa == null) 
                return NotFound();

            return Ok(tarefa);

           // return Ok(tarefa == null ? NotFound() : tarefa);            

        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            return Ok(_context.Tarefas.ToList());               
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            if (tarefa == null)
                return NotFound();
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);

            return Ok(tarefa == null? NotFound(): tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {           
            var tarefa = _context.Tarefas.Where(x => x.Status == status);

            return Ok(tarefa == null? NotFound(): tarefa);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Add(tarefa);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefas = _context.Tarefas.Find(id);

            if (tarefas == null)    
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            tarefas.Titulo = tarefa.Titulo;
            tarefas.Descricao = tarefa.Descricao;
            tarefas.Data = tarefa.Data;
            tarefas.Status = tarefa.Status;

            _context.SaveChanges();
          
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}
