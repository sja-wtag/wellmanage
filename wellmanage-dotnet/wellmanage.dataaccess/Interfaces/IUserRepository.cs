using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.domain.Entity;

namespace wellmanage.data.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task MarkCheckIn(long userId);
        public Task MarkCheckOut(long userId);
        Task<bool> IsAlreadyCheckedIn(long userId);
        Task<(bool IsAlreadyCheckedIn, bool IsAlreadyCheckedOut)> CheckAttendanceStatus(long userId);
    }
}
