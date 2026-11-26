
namespace FocusFlow.DAL.Repo.Implementation;

public class AuditEventRepository : IAuditEventRepository
{
    private readonly FocusFlowDbContext DB;

    public AuditEventRepository(FocusFlowDbContext Db)
    {
        DB = Db;
    }

    public async Task<AuditEvent?> GetByIdAsync(long id)
    {
        return await DB.AuditEvents
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<AuditEvent>> GetByActorIdAsync(string actorId)
    {
        return await DB.AuditEvents
            .Where(a => a.ActorId == actorId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<AuditEvent>> GetAllAsync()
    {
        return await DB.AuditEvents.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task AddAsync(AuditEvent auditEvent)
    {
        await DB.AuditEvents.AddAsync(auditEvent);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await DB.SaveChangesAsync() > 0;
    }
}