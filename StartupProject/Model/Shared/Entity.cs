namespace Startup.Domain.Shared
{
    public abstract class Entity
    {
        public Guid Id { get; init; }

        public Entity()
        {
            this.Id = Guid.NewGuid();
        }

        public Entity(Guid id) 
        {
           Id = id;   
        }
    }
}
