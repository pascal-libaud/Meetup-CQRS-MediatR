using MediatR;

namespace Api;

public class MyNotificationPublisher : INotificationPublisher
{
    public async Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
    {
        foreach (var handler in handlerExecutors)
        {
            await handler.HandlerCallback(notification, cancellationToken).ConfigureAwait(false);
        }

        //var tasks = handlerExecutors
        //    .Select(handler => handler.HandlerCallback(notification, cancellationToken))
        //    .ToArray();

        //return Task.WhenAll(tasks);
    }
}