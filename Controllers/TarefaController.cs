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

        /// <summary>
        /// Obtém uma tarefa específica pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador numérico da tarefa a ser consultada.</param>
        /// <returns>
        /// <see cref="OkObjectResult"/> contendo a <see cref="Tarefa"/> localizada
        /// ou <see cref="NotFoundResult"/> caso nenhuma tarefa com o <paramref name="id"/> exista.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        /// <summary>
        /// Obtém todas as tarefas cadastradas no banco de dados.
        /// </summary>
        /// <returns>
        /// <see cref="OkObjectResult"/> contendo a lista completa de <see cref="Tarefa"/>.
        /// Retorna lista vazia quando não há tarefas cadastradas.
        /// </returns>
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

        /// <summary>
        /// Obtém as tarefas cujo título contenha o trecho informado (busca parcial, case-sensitive conforme collation do banco).
        /// </summary>
        /// <param name="titulo">Trecho do título a ser pesquisado.</param>
        /// <returns>
        /// <see cref="OkObjectResult"/> contendo a lista de <see cref="Tarefa"/> que atendem ao filtro.
        /// </returns>
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo)).ToList();
            return Ok(tarefas);
        }

        /// <summary>
        /// Obtém as tarefas cuja data seja igual à data informada (compara apenas a parte de data, ignora hora).
        /// </summary>
        /// <param name="data">Data usada como filtro.</param>
        /// <returns>
        /// <see cref="OkObjectResult"/> contendo a lista de <see cref="Tarefa"/> do dia informado.
        /// </returns>
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();
            return Ok(tarefa);
        }

        /// <summary>
        /// Obtém as tarefas que possuem o status informado (Pendente ou Finalizado).
        /// </summary>
        /// <param name="status">Valor de <see cref="EnumStatusTarefa"/> usado como filtro.</param>
        /// <returns>
        /// <see cref="OkObjectResult"/> contendo a lista de <see cref="Tarefa"/> com o status informado.
        /// </returns>
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefa = _context.Tarefas.Where(x => x.Status == status).ToList();
            return Ok(tarefa);
        }

        /// <summary>
        /// Cria uma nova tarefa no banco de dados.
        /// </summary>
        /// <param name="tarefa">Objeto <see cref="Tarefa"/> recebido no corpo da requisição.</param>
        /// <returns>
        /// <see cref="CreatedAtActionResult"/> apontando para <see cref="ObterPorId(int)"/> com a tarefa criada,
        /// ou <see cref="BadRequestObjectResult"/> se a data for inválida.
        /// </returns>
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        /// <summary>
        /// Atualiza uma tarefa existente no banco de dados.
        /// </summary>
        /// <param name="id">Identificador da tarefa a ser atualizada.</param>
        /// <param name="tarefa">Objeto <see cref="Tarefa"/> com os novos valores.</param>
        /// <returns>
        /// <see cref="OkObjectResult"/> com a tarefa atualizada, <see cref="NotFoundResult"/> quando o
        /// <paramref name="id"/> não é encontrado, ou <see cref="BadRequestObjectResult"/> quando a data é inválida.
        /// </returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok(tarefaBanco);
        }

        /// <summary>
        /// Remove uma tarefa do banco de dados pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador da tarefa a ser removida.</param>
        /// <returns>
        /// <see cref="NoContentResult"/> em caso de sucesso ou <see cref="NotFoundResult"/> se a tarefa não existir.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
