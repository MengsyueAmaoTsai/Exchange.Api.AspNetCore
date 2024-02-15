namespace RichillCapital.UseCases.Common;

public interface INotificationService
{
    Task SendLineNotificationAsync(string message);
}