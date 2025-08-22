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
            // TODO: Buscar o Id no banco utilizando o EF
            // var tarefa = _context.Tarefas.Find(id);
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            // if (tarefa == null)
                // return NotFound();

            // return Ok(tarefa);

            try
            {
                var tarefa = _context.Tarefas.Find(id);
                if (tarefa == null)
                    return NotFound();

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = "Erro ao buscar a tarefa", Detalhes = ex.Message });
            }
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            try
            {
                var tarefas = _context.Tarefas.ToList();
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = "Erro ao buscar as tarefas", Detalhes = ex.Message });
            }
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            try
            {
                var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = "Erro ao buscar as tarefas", Detalhes = ex.Message });
            }
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData([FromQuery] DateTime data)
        {
            try
            {
                var dataSomente = data.Date;
                var tarefas = _context.Tarefas.Where(x => x.Data.Date == dataSomente).ToList();
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = "Erro ao buscar as tarefas", Detalhes = ex.Message });
            }
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            try
            {
                var tarefas = _context.Tarefas.Where(x => x.Status == status);
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = "Erro ao buscar as tarefas", Detalhes = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

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

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
