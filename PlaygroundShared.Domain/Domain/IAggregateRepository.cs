﻿namespace PlaygroundShared.Domain.Domain;

public interface IAggregateRepository<TAggregate> : IRepository where TAggregate : BaseAggregateRoot
{
    Task PersistAsync(TAggregate aggregate);
    Task DeleteAsync(TAggregate aggregate);
    Task<TAggregate> GetAsync(AggregateId id);
}