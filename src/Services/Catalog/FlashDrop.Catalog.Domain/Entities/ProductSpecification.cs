namespace FlashDrop.Catalog.Domain.Entities;

public class ProductSpecification
{
    public Guid Id { get; private set; }

    public Guid ProductId { get; private set; }

    public string Name { get; private set; } = null!;

    public string Value { get; private set; } = null!;

    public int DisplayOrder { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    private ProductSpecification()
    {
    }

    public ProductSpecification(
        Guid productId,
        string name,
        string value,
        int displayOrder = 0)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Name = name;
        Value = value;
        DisplayOrder = displayOrder;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
        string name,
        string value)
    {
        Name = name;
        Value = value;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeDisplayOrder(int displayOrder)
    {
        DisplayOrder = displayOrder;
        UpdatedAt = DateTime.UtcNow;
    }
}