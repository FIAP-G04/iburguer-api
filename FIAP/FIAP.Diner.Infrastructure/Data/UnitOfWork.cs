using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Infrastructure.Data;

public interface IUnitOfWork<T>: IDisposable where T : Entity<Id>
{
    DbSet<T> Set();

    Task SaveAsync(T aggregate, CancellationToken cancellation);

    Task UpdateAsync(T aggregate, CancellationToken cancellation);
}


public class UnitOfWork<T> : IUnitOfWork<T> where T : Entity<Id>
{
    private readonly Context _context;
    private readonly IEventDispatcher _dispatcher;

    public UnitOfWork(Context context, IEventDispatcher dispatcher)
    {
        _context = context;
        _dispatcher = dispatcher;
    }

    public DbSet<T> Set()
    {
        return _context.Set<T>();
    }

    public async Task SaveAsync(T aggregate, CancellationToken cancellation)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                await Set().AddAsync(aggregate, cancellation);

                await ApplyChanges(aggregate, cancellation);

                transaction.Commit();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task UpdateAsync(T aggregate, CancellationToken cancellation)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                Set().Update(aggregate);

                await ApplyChanges(aggregate, cancellation);

                transaction.Commit();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    private async Task ApplyChanges(T aggregate, CancellationToken cancellation)
    {
        foreach (var @event in aggregate.Events)
        {
            await _dispatcher.Dispatch(@event, cancellation);
        }

        await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            this.disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}