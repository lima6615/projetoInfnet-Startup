namespace StartupProject.Dtos
{
    public class QuizDTO
    {
        public Guid Id { get; set; }
        public string title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool InAtivo { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
