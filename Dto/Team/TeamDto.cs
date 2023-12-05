namespace Kanban.Dto;

public class TeamDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string OwnerId { get; set; } = null!;
}