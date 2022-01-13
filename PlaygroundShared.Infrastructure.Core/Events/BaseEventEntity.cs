﻿namespace PlaygroundShared.Infrastructure.Core.Events;

public abstract class BaseEventEntity
{
    public virtual Guid Id { get; set; }
    public Guid AggregateId { get; set; }
    public string EventType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string Event { get; set; }
    public string ExecutedBy { get; set; }
    public Guid CorrelationId { get; set; }
}