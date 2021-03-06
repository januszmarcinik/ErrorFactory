﻿using System;
using System.Reflection;

namespace ErrorFactory.Core.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IDependencyResolver _dependencyResolver;

        public Mediator(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public Result Command<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _dependencyResolver.ResolveOrDefault<ICommandHandler<TCommand>>();
            if (handler == null)
            {
                throw new InvalidOperationException($"Command of type '{command.GetType()}' has not registered handler.");
            }

            return handler.Handle(command);
        }

        public Result<TResult> Query<TResult>(IQuery<TResult> query)
        {
            // ReSharper disable once PossibleNullReferenceException
            return (Result<TResult>)GetType()
                .GetMethod("Query", BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(query.GetType(), typeof(TResult))
                .Invoke(this, new object[] { query });
        }

        private Result<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _dependencyResolver.ResolveOrDefault<IQueryHandler<TQuery, TResult>>();
            if (handler == null)
            {
                throw new InvalidOperationException($"Query of type '{query.GetType()}' has not registered handler.");
            }

            return handler.Handle(query);
        }
    }
}
