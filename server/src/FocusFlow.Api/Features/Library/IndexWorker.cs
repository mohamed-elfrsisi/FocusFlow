// File      : server/src/FocusFlow.Api/Features/Library/IndexWorker.cs
// Developer : Yasmeen Ayman (M3)
// Reviewer  : Saeid El-Kholy (M4)
// Task(s)   : T025  -  Background text extraction into page chunks (hos
// Module    : FR-05 (FSD §9)
// Purpose   : BackgroundService that reads the queue and calls LibraryIndexer.
namespace FocusFlow.Api.Features.Library;

public sealed class IndexWorker(
    IndexQueue queue,
    IServiceScopeFactory scopes,
    ILogger<IndexWorker> log) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await foreach (var job in queue.ReadAllAsync(ct))
        {
            try
            {
                using var scope = scopes.CreateScope();
                await scope.ServiceProvider
                    .GetRequiredService<LibraryIndexer>()
                    .IndexPdfAsync(job.ItemId, job.Pdf);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "PDF indexing failed for {Item}", job.ItemId);
            }
        }
    }
}