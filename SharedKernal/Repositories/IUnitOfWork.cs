namespace SharedKernal.Repositories;

public interface IUnitOfWork
{
    void SaveChanges();
    Task SaveChangesAsync();
}