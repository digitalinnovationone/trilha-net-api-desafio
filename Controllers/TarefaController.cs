using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using Microsoft.EntityFrameworkCore;

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

        // GET: /Tarefa/{id}
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // Buscar o Id no banco utilizando o EF
            var tarefa = _context.Tarefas.Find(id);

            // Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            if (tarefa == null)
                return NotFound(); // Retorna 404 se a tarefa não for encontrada

            return Ok(tarefa); // Retorna 200 OK com a tarefa
        }


        // GET: /Tarefa/ObterTodos
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // Buscar todas as tarefas no banco utilizando o EF
            var tarefas = _context.Tarefas.ToList();
            return Ok(tarefas); // Retorna 200 OK com todas as tarefas
        }

        // GET: /Tarefa/ObterPorTitulo
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // Buscar as tarefas no banco utilizando o EF, que contenha o título recebido por parâmetro
            var tarefas = _context.Tarefas.Where(t => t.Titulo.Contains(titulo)).ToList();
            return Ok(tarefas); // Retorna 200 OK com as tarefas filtradas pelo título
        }

        // GET: /Tarefa/ObterPorData
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            // Buscar as tarefas por data no banco utilizando o EF
            var tarefas = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();
            return Ok(tarefas); // Retorna 200 OK com as tarefas filtradas pela data
        }

        // GET: /Tarefa/ObterPorStatus
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // Buscar as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            var tarefas = _context.Tarefas.Where(x => x.Status == status).ToList();
            return Ok(tarefas); // Retorna 200 OK com as tarefas filtradas pelo status
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Adicionar a tarefa recebida no EF e salvar as mudanças
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            // Retorna 201 Created com o caminho para a tarefa criada
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            /// Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            // Atualizar a variável tarefaBanco no EF e salvar as mudanças
            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok(tarefaBanco); // Retorna 200 OK com a tarefa atualizada
        }


        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // Remover a tarefa encontrada através do EF e salvar as mudanças
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            return NoContent(); // Retorna 204 No Content após a exclusão
        }
    }
}
