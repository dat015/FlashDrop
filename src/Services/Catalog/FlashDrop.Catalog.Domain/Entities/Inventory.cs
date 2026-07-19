namespace FlashDrop.Catalog.Domain.Entities;

public class Inventory
{
    public Guid Id { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public int ReservedQuantity { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public int AvailableQuantity =>
        Quantity - ReservedQuantity;

    private Inventory()
    {
    }

    public Inventory(
        Guid productId,
        int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException(
                "Quantity cannot be negative.",
                nameof(quantity));

        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        ReservedQuantity = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                "Quantity must be greater than zero.",
                nameof(quantity));

        Quantity += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                "Quantity must be greater than zero.",
                nameof(quantity));

        if (AvailableQuantity < quantity)
            throw new InvalidOperationException(
                "Insufficient stock.");

        Quantity -= quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reserve(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                "Quantity must be greater than zero.",
                nameof(quantity));

        if (AvailableQuantity < quantity)
            throw new InvalidOperationException(
                "Insufficient stock.");

        ReservedQuantity += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ReleaseReservation(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                "Quantity must be greater than zero.",
                nameof(quantity));

        if (ReservedQuantity < quantity)
            throw new InvalidOperationException(
                "Invalid reserved quantity.");

        ReservedQuantity -= quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ConfirmReservation(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                "Quantity must be greater than zero.",
                nameof(quantity));

        if (ReservedQuantity < quantity)
            throw new InvalidOperationException(
                "Invalid reserved quantity.");

        ReservedQuantity -= quantity;
        Quantity -= quantity;

        UpdatedAt = DateTime.UtcNow;
    }
}