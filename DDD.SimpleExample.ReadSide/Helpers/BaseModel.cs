using System;

namespace DDD.SimpleExample.ReadSide.Helpers
{
    public abstract class BaseModel
    {
        public string Id { get; set; }

        protected BaseModel()
        {
        }

        protected BaseModel(Guid id)
        {
            Id = MakeId(GetType(), id);
            AggregateId = id;
        }

        public Guid AggregateId { get; set; }
        public long Identity { get; set; }
        public byte[] RowVersion { get; set; }

        public static string MakeId(Type type, Guid id)
        {
            return string.Concat(type.Name, "-", id);
        }
    }
}