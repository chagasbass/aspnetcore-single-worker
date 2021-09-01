using MongoDB.Bson;

namespace Aspnetcore.SingleWorker.Domain.Entities
{
    public abstract class Entity
    {
        public ObjectId Id { get; private set; }

        protected Entity() { }
    }
}
