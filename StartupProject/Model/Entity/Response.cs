using Startup.Domain.Shared;

namespace Startup.Domain.Entity
{
    public class Response : Shared.Entity
    {
        public Response() { }

        public DateTime DateResponse { get; set; }
        public string Value { get; set; }

        public Alternative alternative { get; set; }
    }
}
