namespace OdevApi.Data;

public interface IUnitOfWork : IDisposable
{

    IGenericRepository<Account> AccountRepository { get; }
    IGenericRepository<Person> PersonRepository { get; }

    void CompleteWithTransaction();
    void Complete();
}
