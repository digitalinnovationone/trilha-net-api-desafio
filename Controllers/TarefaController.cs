using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using System.Linq;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // Buscar o ID no banco de dados
            var tarefa = _context.Tarefas.Find(id);

            // Se não encontrar, retornar NotFound
            if (tarefa == null)
                return NotFound();

            // Caso contrário, retornar OK com a tarefa encontrada
            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // Buscar todas as tarefas no banco
            var tarefas = _context.Tarefas.ToList();

            // Retornar OK com a lista de tarefas
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // Buscar tarefas que contenham o título especificado
            var tarefas = _context.Tarefas.Where(t => t.Titulo.Contains(titulo)).ToList();

            // Retornar OK com a lista de tarefas encontradas
            return Ok(tarefas);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            // Buscar tarefas com a mesma data (desconsiderando o horário)
            var tarefas = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();

            // Retornar OK com as tarefas encontradas
            return Ok(tarefas);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // Buscar tarefas com o status especificado
            var tarefas = _context.Tarefas.Where(x => x.Status == status).ToList();

            // Retornar OK com as tarefas encontradas
            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Adicionar a nova tarefa ao banco
            _context.Tarefas.Add(tarefa);

            // Salvar as mudanças
            _context.SaveChanges();

            // Retornar CreatedAtAction com os dados da nova tarefa
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            // Buscar a tarefa no banco pelo ID
            var tarefaBanco = _context.Tarefas.Find(id);

            // Se não encontrar, retornar NotFound
            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Atualizar os dados da tarefaBanco com os dados recebidos
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            // Salvar as mudanças no banco
            _context.SaveChanges();

            // Retornar OK
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            // Buscar a tarefa no banco pelo ID
            var tarefaBanco = _context.Tarefas.Find(id);

            // Se não encontrar, retornar NotFound
            if (tarefaBanco == null)
                return NotFound();

            // Remover a tarefa encontrada
            _context.Tarefas.Remove(tarefaBanco);

            // Salvar as mudanças no banco
            _context.SaveChanges();

            // Retornar NoContent
            return NoContent();
        }
    }
}
