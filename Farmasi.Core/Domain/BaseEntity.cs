using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farmasi.Core.Domain
{
    public class BaseEntity
    {
        //[
        //[BsonId] public int Id { get; set; }
        //[BsonId]
        [BsonId] public Guid Id  { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            Id = Guid.NewGuid();
        }
    }
}

