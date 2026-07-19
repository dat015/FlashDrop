namespace FlashDrop.Catalog.Domain.Entities;

public class ProductImage
{
    public Guid Id { get; private set; }

    public Guid ProductId { get; private set; }

    public string ImageUrl { get; private set; } = null!;

    public bool IsPrimary { get; private set; }

    public int DisplayOrder { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    private ProductImage()
    {
    }

    public ProductImage(
        Guid productId,
        string imageUrl,
        bool isPrimary = false,
        int displayOrder = 0)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ImageUrl = imageUrl;
        IsPrimary = isPrimary;
        DisplayOrder = displayOrder;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateImage(string imageUrl)
    {
        ImageUrl = imageUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetAsPrimary()
    {
        IsPrimary = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemovePrimary()
    {
        IsPrimary = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeDisplayOrder(int displayOrder)
    {
        DisplayOrder = displayOrder;
        UpdatedAt = DateTime.UtcNow;
    }
}