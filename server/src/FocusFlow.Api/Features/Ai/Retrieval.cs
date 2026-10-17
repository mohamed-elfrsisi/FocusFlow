// File      : server/src/FocusFlow.Api/Features/Ai/Retrieval.cs
// Developer : Yasmeen Ayman (M3)
// Reviewer  : Saeid El-Kholy (M4)
// Task(s)   : T036, T056  -  Full-text index on chunks + ranked search query
// Module    : FR-05 (FSD §9), FR-06 (FSD §9)
// Purpose   : Keyword extraction and top-5 chunk retrieval with CONTAINSTABLE (parameterized SQL only).
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using FocusFlow.Api.Data;

namespace FocusFlow.Api.Features.Ai;

public sealed class ChunkHit
{
    public string Content { get; set; } = "";
    public int Page { get; set; }
    public Guid ItemId { get; set; }
    public string Title { get; set; } = "";
    public int Rank { get; set; }
}

public sealed class Retrieval(FocusFlowDbContext db)
{
    public static List<string> Keywords(string q) =>
        Regex.Split(q.ToLowerInvariant(), @"[^\p{L}\p{N}]+")
            .Where(w => w.Length > 2).Distinct().Take(12).ToList();

    public async Task<List<ChunkHit>> RetrieveAsync(
        Guid studentId, Guid[] fileIds, string question, int k = 5)
    {
        var kw = Keywords(question);
        if (kw.Count == 0 || fileIds.Length == 0) return [];

        var search = string.Join(" OR ", kw.Select(w => $"\"{w}\""));
        var ids = JsonSerializer.Serialize(fileIds);

        return await db.Database.SqlQuery<ChunkHit>($@"
            SELECT TOP ({k}) c.content AS Content, c.page AS Page, i.id AS ItemId,
                   i.title AS Title, ft.[RANK] AS [Rank]
            FROM CONTAINSTABLE(library_chunks, content, {search}) AS ft
            JOIN library_chunks c ON c.id = ft.[KEY]
            JOIN library_items i ON i.id = c.item_id
            WHERE i.student_id = {studentId}
              AND i.id IN (SELECT [value] FROM OPENJSON({ids}) WITH ([value] uniqueidentifier '$'))
            ORDER BY ft.[RANK] DESC").ToListAsync();
    }
}