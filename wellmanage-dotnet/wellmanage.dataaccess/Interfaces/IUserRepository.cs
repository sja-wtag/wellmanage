using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.domain.Entity;

namespace wellmanage.data.Interfaces
{
    internal interface IUserRepository : IGenericRepository<User>
    {
        public Task MarkCheckIn(int userId);
        public Task MarkCheckOut(int userId);
    }
}
