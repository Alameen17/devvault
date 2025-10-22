namespace DevVault.Application.Tasks.DTOs
{
    public class TaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
