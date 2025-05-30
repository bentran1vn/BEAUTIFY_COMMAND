﻿using System.ComponentModel.DataAnnotations;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class Category : AggregateRoot<Guid>, IAuditableEntity
{
    [MaxLength(100)] public required string Name { get; set; }
    [MaxLength(int.MaxValue)] public string? Description { get; set; }
    public bool IsParent { get; set; } = false;
    public Guid? ParentId { get; set; }
    public virtual Category? Parent { get; set; }
    public virtual ICollection<Category> Children { get; set; } = [];
    public virtual ICollection<Service> Services { get; set; } = [];
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
}