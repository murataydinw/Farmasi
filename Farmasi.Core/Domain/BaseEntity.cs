using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farmasi.Core.Domain
{
    public class BaseEntity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[BsonId] public int Id { get; set; }
        [BsonId]
        public Guid Uid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public BaseEntity()
        {
            Uid = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

    }
}

