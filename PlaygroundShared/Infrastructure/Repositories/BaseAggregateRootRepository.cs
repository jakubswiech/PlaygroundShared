﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using PlaygroundShared.Domain;
using PlaygroundShared.Infrastructure.Persistance;

namespace PlaygroundShared.Infrastructure.Repositories
{
    public abstract class BaseAggregateRootRepository<TAggregate, TEntity> : IAggregateRepository<TAggregate> where TAggregate : BaseAggregateRoot where TEntity : BaseDbEntity
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IAggregateRecreate<TAggregate> _aggregateRecreate;

        protected BaseAggregateRootRepository(IGenericRepository<TEntity> repository, IMapper mapper, IAggregateRecreate<TAggregate> aggregateRecreate)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _aggregateRecreate = aggregateRecreate ?? throw new ArgumentNullException(nameof(aggregateRecreate));
        }
        
        public virtual async Task PersistAsync(TAggregate aggregate)
        {
            var mappedEntity = MapToEntity(aggregate);
            if (await _repository.ExistsAsync(x => x.Id == aggregate.Id.ToGuid()))
            {
                await _repository.UpdateAsync(mappedEntity);
            }
            else
            {
                await _repository.AddAsync(mappedEntity);
            }
        }

        public virtual async Task DeleteAsync(TAggregate aggregate)
        {
            var mappedEntity = MapToEntity(aggregate);
            await _repository.DeleteAsync(mappedEntity);
        }

        public virtual async Task<TAggregate> GetAsync(AggregateId id) => MapToAggregate(await _repository.GetAsync(id.ToGuid()));

        protected virtual TEntity MapToEntity(TAggregate aggregate) => _mapper.Map<TEntity>(aggregate);

        protected virtual TAggregate MapToAggregate(TEntity entity)
        {
            var aggregate = _mapper.Map<TAggregate>(entity);
            _aggregateRecreate.Init(aggregate);

            return aggregate;
        } 
    }
}