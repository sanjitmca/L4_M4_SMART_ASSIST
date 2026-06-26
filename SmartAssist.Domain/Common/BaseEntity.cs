namespace SmartAssist.Domain.Common
{
    public abstract class BaseEntity
    {
        // Data type: DateTime — set once at creation
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

        // Nullable reference type: UpdatedAt is null until the entity is first changed
        public DateTime? UpdatedAt { get; protected set; }

        // Method: encapsulates the "stamp the update time" behaviour so subclasses call it
        protected void MarkUpdated() => UpdatedAt = DateTime.UtcNow;
    }
}
