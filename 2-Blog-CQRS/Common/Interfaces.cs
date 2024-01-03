using MediatR;

namespace _2_Blog_CQRS.Common;

public interface IQuery<out T> : IRequest<T> { }

public interface ICommand : IRequest { }

public interface ICommand<out T> : IRequest<T> { }