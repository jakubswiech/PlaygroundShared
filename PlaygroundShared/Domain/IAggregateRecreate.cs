﻿namespace PlaygroundShared.Domain
{
    public interface IAggregateRecreate<in TAggregate> where TAggregate : BaseAggregateRoot
    {
        void Init(TAggregate aggregate);
    }
}