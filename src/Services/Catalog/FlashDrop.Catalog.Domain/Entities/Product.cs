namespace FlashDrop.Catalog.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }

    public Guid CategoryId { get; private set; }

    public string Name { get; private set; } = null!;

    public string Slug { get; private set; } = null!;

    public string? Description { get; private set; }

    public string SKU { get; private set; } = null!;

    public decimal Price { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    private Product()
    {
    }

    public Product(
        Guid categoryId,
        string name,
        string slug,
        string sku,
        decimal price,
        string? description = null)
    {
        Id = Guid.NewGuid();
        CategoryId = categoryId;
        Name = name;
        Slug = slug;
        SKU = sku;
        Price = price;
        Description = description;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
        string name,
        string slug,
        string? description)
    {
        Name = name;
        Slug = slug;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeCategory(Guid categoryId)
    {
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentException(
                "Price cannot be negative.",
                nameof(price));

        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}