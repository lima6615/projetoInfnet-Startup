namespace StartupProject.Dtos
{
    namespace StartupProject.Dtos
    {
        public class QuestionDTO
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public string? Type { get; set; }
            public List<AlternativeDTO>? Alternatives { get; set; }
        }
    }
}
