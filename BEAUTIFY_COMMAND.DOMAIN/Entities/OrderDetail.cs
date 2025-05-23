﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class OrderDetail : AggregateRoot<Guid>, IAuditableEntity
{
    public Guid OrderId { get; set; }
    public virtual Order? Order { get; set; }
    public Guid ProcedurePriceTypeId { get; set; }
    public virtual ProcedurePriceType? ProcedurePriceType { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
}