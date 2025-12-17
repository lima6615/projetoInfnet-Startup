namespace Startup.Domain.Entity
{
    public class Quiz : Shared.Entity
    {     

        public Quiz() { }

        public string title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Boolean InAtivo { get; set;}
        public DateTime CreatedAt { get; set; }
        public List<Question> _Perguntas { get; set; } = new List<Question>();
    }
}
