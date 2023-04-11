using MediatR;

namespace Core.Common;

public interface IQuery<out T> : IRequest<T> { }

public interface ICommand : IRequest { }

public interface ICommand<out T> : IRequest<T> { }