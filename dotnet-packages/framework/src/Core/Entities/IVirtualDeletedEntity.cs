namespace Framework.Core.Entities
{
    public interface IVirtualDeletedEntity
    {
        bool IsDeleted { get; set; }
    }
}
