namespace FocusFlow.DAL.Repo;

public interface IAuditEventRepository
{
    
    //use Task<> to make implementation later

    Task<AuditEvent?> GetByIdAsync(long id);

    //use IEnumerable to make iteration later when GetAll
    Task<IEnumerable<AuditEvent>> GetByActorIdAsync(string actorId);

    Task<IEnumerable<AuditEvent>> GetAllAsync();

    Task AddAsync(AuditEvent auditEvent);

    Task<bool> SaveChangesAsync();
}






