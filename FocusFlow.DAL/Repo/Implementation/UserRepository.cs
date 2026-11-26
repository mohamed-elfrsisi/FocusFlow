
namespace FocusFlow.DAL.Repo.Implementation;

public class UserRepository : IUserRepository
{
    private readonly FocusFlowDbContext DB;
    
    public UserRepository(FocusFlowDbContext Db)
    {
        DB = Db;
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await DB.Users.FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await DB.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.DeletedAt == null);
    }

   public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await DB.Users.Where(u => u.DeletedAt == null).ToListAsync();
    }

    /// <summary>
    /// Checks whether a non-deleted user exists with the specified email.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>True if a matching user exists; otherwise, false.</returns>
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await DB.Users
            .AnyAsync(u => u.Email == email && u.DeletedAt == null);
    }

    public async Task AddAsync(User user)
    {
        await DB.Users.AddAsync(user);
    }

   
    public void Update(User user)
    {
        DB.Users.Update(user);
    }
    
    
    public void Delete(User user)
    {
        user.DeletedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Saves all pending changes to the database.
    /// </summary>
    /// <returns>
    /// True if at least one record was affected otherwise = false.
    /// </returns>
    public async Task<bool> SaveChangesAsync()
    {
        return await DB.SaveChangesAsync() > 0;
    }
}
