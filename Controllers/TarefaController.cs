using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult ObterPorId(int id) //OK
        {
            var tarefa = _context.Tarefas.Find(id) ;
            if (tarefa is null){
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos() //OK
        {
            var tarefa = _context.Tarefas.ToList();
            return Ok(tarefa);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo) //OK
        {
            var tarefa = _context.Tarefas.Find(titulo);
            if (tarefa is null){
                return NotFound();
            }
            return Ok(titulo);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data) //OK
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status) //OK
        {
            var tarefa = _context.Tarefas.Find(status);
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa) //OK
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa) //OK
        {
            var tarefaBanco = _context.Tarefas.Find(id);
            if (tarefaBanco == null){
                return NotFound();
            }
            if (tarefa.Data == DateTime.MinValue) {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;
            _context.SaveChanges();
            return Ok(tarefa);    
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id) //OK
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco is null)
                return NotFound();
            
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
