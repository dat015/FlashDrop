using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Catalog.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = null!;

        public string Slug { get; private set; } = null!;

        public string? Description { get; private set; }

        public Guid? ParentId { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        private Category()
        {
        }

        public Category(
            string name,
            string slug,
            string? description = null,
            Guid? parentId = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Slug = slug;
            Description = description;
            ParentId = parentId;
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

        public void ChangeParent(Guid? parentId)
        {
            ParentId = parentId;
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
}
