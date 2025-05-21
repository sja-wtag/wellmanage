using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage.data.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<Attendance> MarkCheckIn(long userId);
        public Task<Attendance> MarkCheckOut(long userId);
        Task<bool> IsAlreadyCheckedIn(long userId);
        Task<(bool IsAlreadyCheckedIn, bool IsAlreadyCheckedOut)> CheckAttendanceStatus(long userId);
        Task<AttendanceStatus> GetAttendanceStatus(long userId);
        Task<List<Attendance>> GetAttendances(long userId);
        Task<List<UserInfo>> GetUsersWhoAreNotEmployee();
        Task<List<AttendanceDto>> GetAttendancesToday();
    }
}
