using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using wellmanage.data.Data;
using wellmanage.data.Interfaces;

namespace wellmanage.data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _transactionContext;
        private IDbContextTransaction _transaction;
        public UnitOfWork(DataContext databaseContext)
        {
            _transactionContext = databaseContext;
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _transactionContext.Database.BeginTransactionAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _transactionContext.SaveChangesAsync();
        }
        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }
        public async Task RollBackAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
