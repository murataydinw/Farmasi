namespace Farmasi.Core.Domain.Common
{
    public interface ISoftDeletedEntity
    {
        bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }       
        
    }
}

