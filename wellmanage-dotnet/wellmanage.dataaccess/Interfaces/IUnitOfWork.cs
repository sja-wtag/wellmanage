﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wellmanage.data.Interfaces
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CommitAsync();
        Task RollBackAsync();
    }
}
