// File      : server/src/FocusFlow.Api/Features/Library/IndexQueue.cs
// Developer : Yasmeen Ayman (M3)
// Reviewer  : Saeid El-Kholy (M4)
// Task(s)   : T025  -  Background text extraction into page chunks (hos
// Module    : FR-05 (FSD §9)
// Purpose   : In-process Channel<IndexJob> queue for PDF indexing (MVP).
using System.Threading.Channels;

namespace FocusFlow.Api.Features.Library;

public sealed record IndexJob(Guid ItemId, byte[] Pdf);

public sealed class IndexQueue
{
    private readonly Channel<IndexJob> _ch = Channel.CreateUnbounded<IndexJob>();

    public void Enqueue(IndexJob job) => _ch.Writer.TryWrite(job);

    public IAsyncEnumerable<IndexJob> ReadAllAsync(CancellationToken ct) =>
        _ch.Reader.ReadAllAsync(ct);
}