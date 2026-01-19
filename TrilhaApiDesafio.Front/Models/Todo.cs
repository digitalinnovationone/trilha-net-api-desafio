using System.ComponentModel.DataAnnotations;

namespace TrilhaApiDesafio.Front.Models;

public class Todo
{
    public int Id { get; set; }

    [Required]
    [StringLength(80, MinimumLength = 2)]
    public string Task { get; set; } = "";

    [StringLength(500)]
    public string Description { get; set; } = "";

    [Required]
    public DateTime Deadline { get; set; } = DateTime.Today;

    public EnumChoreStatus Status { get; set; } = EnumChoreStatus.Pending;
}