using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private const string connectionString = "Server=localhost\\sqlexpress; Initial Catalog=Tarefa; Integrated Security=true";

        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }
        // Salvar - Criar novo
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Add(tarefa);
            _context.SaveChanges();

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            return CreatedAtAction(nameof(ObterPorId), new {id=tarefa.Id}, tarefa);
        }
        //elecionar por id
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            var tarefa  = _context.Tarefas?.Find(id);
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            if(tarefa == null)
                return NotFound();

            // caso contrário retornar OK com a tarefa encontrada
            return Ok(tarefa);
        }
        // seçecionar todos
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            //var tarefa  = _context.Tarefas.Find(*);
            
            var tarefas =  _context.Tarefas?.Where(x => x.Id > 0);
            return Ok(tarefas);
        }
        // Selecionar pelo título
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            var tarefas =  _context.Tarefas?.Where(x => x.Titulo.Contains(titulo));
            // Dica: Usar como exemplo o endpoint ObterPorData
            return Ok(tarefas);
        }
        // selecionar por data aaaa-mm-dd
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas?.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }
        //Selecionar por status
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            var tarefa = _context.Tarefas?.Where(x => x.Status == status);
            // Dica: Usar como exemplo o endpoint ObterPorData
            return Ok(tarefa);
        }
        // Update - Aterar poi id
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas?.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Status = tarefa.Status;
            tarefaBanco.Data = tarefa.Data;

            _context.Tarefas?.Update(tarefaBanco);
            _context.SaveChanges();
            
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas?.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Tarefas?.Remove(tarefaBanco);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
