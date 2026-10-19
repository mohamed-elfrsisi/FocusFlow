// File      : server/src/FocusFlow.Api/Features/Library/LibraryIndexer.cs
// Developer : Yasmeen Ayman (M3)
// Reviewer  : Saeid El-Kholy (M4)
// Task(s)   : T025, T035  -  Background text extraction into page chunks (hos
// Module    : FR-05 (FSD §9)
// Purpose   : PdfPig text extraction per page and chunking (about 900 chars, 150 overlap); sets searchable=true.
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using FocusFlow.Api.Data;
using FocusFlow.Api.Data.Entities;

namespace FocusFlow.Api.Features.Library;

public sealed record Chunk(int Page, string Content);

public sealed class LibraryIndexer(FocusFlowDbContext db)
{
    public async Task IndexPdfAsync(Guid itemId, byte[] pdf)
    {
        using var doc = UglyToad.PdfPig.PdfDocument.Open(pdf);
        var chunks = ChunkPages(doc.GetPages().Select(p => p.Text).ToList());
        if (chunks.Count == 0) return; // PDF متصور: searchable يفضل false

        db.LibraryChunks.AddRange(chunks.Select((c, i) =>
            new LibraryChunk { ItemId = itemId, Idx = i, Page = c.Page, Content = c.Content }));
        await db.SaveChangesAsync();

        await db.LibraryItems.Where(i => i.Id == itemId)
            .ExecuteUpdateAsync(s => s.SetProperty(i => i.Searchable, true));
    }

    public static List<Chunk> ChunkPages(IReadOnlyList<string> pages, int size = 900, int overlap = 150)
    {
        var output = new List<Chunk>();
        for (var i = 0; i < pages.Count; i++)
        {
            var clean = Regex.Replace(pages[i], @"\s+", " ").Trim();
            for (var s = 0; s < clean.Length; s += size - overlap)
                output.Add(new Chunk(i + 1, clean.Substring(s, Math.Min(size, clean.Length - s))));
        }
        return output;
    }
}