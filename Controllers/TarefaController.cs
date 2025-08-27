using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using Microsoft.EntityFrameworkCore;

namespace TrilhaApiDesafio.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefaController(OrganizadorContext context) : ControllerBase
{
    /// <summary>
    /// Obtém uma tarefa pelo seu ID.
    /// </summary>
    /// <param name="id">ID da tarefa.</param>
    /// <returns>Retorna a tarefa encontrada ou NotFound se não existir.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var tarefa = await context.Tarefas.FindAsync(id);
        if (tarefa is null)
            return NotFound();
        return Ok(tarefa);
    }

    /// <summary>
    /// Obtém todas as tarefas cadastradas.
    /// </summary>
    /// <returns>Retorna a lista de tarefas.</returns>
    [HttpGet("ObterTodos")]
    public async Task<IActionResult> ObterTodos()
    {
        var tarefas = await context.Tarefas.ToListAsync();
        return Ok(tarefas);
    }

    /// <summary>
    /// Busca tarefas que contenham o título informado (case-insensitive).
    /// </summary>
    /// <param name="titulo">Título parcial ou completo da tarefa.</param>
    /// <returns>Retorna lista de tarefas que contêm o título informado.</returns>
    [HttpGet("ObterPorTitulo")]
    public async Task<IActionResult> ObterPorTitulo(string titulo)
    {
        var tarefas = await context.Tarefas
            .Where(x =>
                EF.Functions.ILike(x.Titulo, $"%{titulo}%"))
            .ToListAsync();
        return Ok(tarefas);
    }

    /// <summary>
    /// Busca tarefas com base na data informada.
    /// </summary>
    /// <param name="data">Data da tarefa (somente parte da data será considerada).</param>
    /// <returns>Retorna lista de tarefas com a data informada.</returns>
    [HttpGet("ObterPorData")]
    public async Task<IActionResult> ObterPorData(DateTime data)
    {
        var tarefas = await context.Tarefas
            .Where(x => x.Data.Date == data.Date)
            .ToListAsync();
        return Ok(tarefas);
    }

    /// <summary>
    /// Busca tarefas com base no status informado.
    /// </summary>
    /// <param name="status">Status da tarefa (por exemplo: Pendente, Concluida, etc).</param>
    /// <returns>Retorna lista de tarefas com o status informado.</returns>
    [HttpGet("ObterPorStatus")]
    public async Task<IActionResult> ObterPorStatus(EnumStatusTarefa status)
    {
        var tarefas = await context.Tarefas
            .Where(x => x.Status == status)
            .ToListAsync();
        return Ok(tarefas);
    }

    /// <summary>
    /// Cria uma nova tarefa.
    /// </summary>
    /// <param name="tarefa">Objeto da tarefa a ser criada.</param>
    /// <returns>Retorna a tarefa criada com status 201.</returns>
    [HttpPost]
    public async Task<IActionResult> Criar(Tarefa tarefa)
    {
        if (tarefa.Data == DateTime.MinValue)
            return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

        await context.Tarefas.AddAsync(tarefa);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
    }

    /// <summary>
    /// Atualiza os dados de uma tarefa existente.
    /// </summary>
    /// <param name="id">ID da tarefa a ser atualizada.</param>
    /// <param name="tarefa">Objeto com os novos dados da tarefa.</param>
    /// <returns>Retorna a tarefa atualizada ou NotFound se não existir.</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, Tarefa tarefa)
    {
        var tarefaBanco = await context.Tarefas.FindAsync(id);

        if (tarefaBanco is null)
            return NotFound();

        if (tarefa.Data == DateTime.MinValue)
            return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

        tarefaBanco.Titulo = tarefa.Titulo;
        tarefaBanco.Descricao = tarefa.Descricao;
        tarefaBanco.Data = tarefa.Data;
        tarefaBanco.Status = tarefa.Status;

        context.Tarefas.Update(tarefaBanco);
        await context.SaveChangesAsync();
        return Ok(tarefaBanco);
    }

    /// <summary>
    /// Deleta uma tarefa existente.
    /// </summary>
    /// <param name="id">ID da tarefa a ser deletada.</param>
    /// <returns>Retorna NoContent se a tarefa foi deletada ou NotFound se não existir.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var tarefaBanco = await context.Tarefas.FindAsync(id);

        if (tarefaBanco is null)
            return NotFound();

        context.Tarefas.Remove(tarefaBanco);
        await context.SaveChangesAsync();
        return NoContent();
    }
}