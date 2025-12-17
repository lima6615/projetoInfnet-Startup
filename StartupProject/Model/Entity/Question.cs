namespace Startup.Domain.Entity
{
    public class Question : Shared.Entity
    {

        public Question() { }

        public string Name { get; set; }
        public string Type { get; set; }
        public Quiz quiz { get; set; }
        public List<Alternative> alternatives { get; set; } = new List<Alternative>();
    }
}
