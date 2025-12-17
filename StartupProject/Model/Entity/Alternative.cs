namespace Startup.Domain.Entity
{
    public class Alternative : Shared.Entity
    {

        public Alternative() { }

        public string Text { get; set; }
        public Question question { get; set; }
        public List<Response> responses { get; set; } = new List<Response>();
    }
}
