
namespace AzureAirlines.Components.Pages;

public class CreateAzureResourceWorker : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Delay(1000, stoppingToken);
        Task.Delay(5000, stoppingToken);
        Task.Delay(10_000, stoppingToken);

        return Task.CompletedTask;
    }
}
