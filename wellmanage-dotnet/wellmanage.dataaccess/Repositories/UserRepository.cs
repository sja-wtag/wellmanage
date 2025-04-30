using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wellmanage.data.Data;
using wellmanage.data.Interfaces;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage.data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public async Task<bool> IsAlreadyCheckedIn(long userId)
        {
            return await _dataContext.Attendances.OrderByDescending(item => item.Id)
                .AnyAsync(item => item.UserId == userId && item.CheckInTime.Date == DateTime.UtcNow.Date && item.CheckOutTime == null);
        }

        public async Task<bool> IsAlreadyCheckedOut(long userId)
        {
            return await _dataContext.Attendances.OrderByDescending(item => item.Id)
                .AnyAsync(item => item.UserId == userId && item.CheckInTime.Date == DateTime.UtcNow.Date && item.CheckOutTime == DateTime.UtcNow.Date);
        }

        public async Task<(bool IsAlreadyCheckedIn, bool IsAlreadyCheckedOut)> CheckAttendanceStatus(long userId)
        {
            var attendanceStatus = await _dataContext.Attendances
                .Where(item => item.UserId == userId && item.CheckInTime.Date == DateTime.UtcNow.Date)
                .OrderByDescending(item => item.Id)
                .FirstOrDefaultAsync();

            if (attendanceStatus == null)
            {
                return (false, false);
            }

            bool isAlreadyCheckedIn = attendanceStatus.CheckOutTime == null;
            bool isAlreadyCheckedOut = attendanceStatus.CheckOutTime?.Date == DateTime.UtcNow.Date;

            return (isAlreadyCheckedIn, isAlreadyCheckedOut);
        }

        public async Task<AttendanceStatus> GetAttendanceStatus(long userId)
        {
            var attendancesToday = await _dataContext.Attendances
                .Where(item => item.UserId == userId && item.CheckInTime.Date == DateTime.UtcNow.Date)
                .OrderByDescending(item => item.Id)
                .ToListAsync();

            var lastAttendance = attendancesToday.FirstOrDefault();

            var totalWorkTime =  attendancesToday
                .Select(item => item.CheckOutTime == null ? DateTime.UtcNow - item.CheckInTime : item.CheckOutTime - item.CheckInTime)
                .Aggregate(TimeSpan.Zero, (sum,next) => (TimeSpan)(sum + next));

            var attendenceDetails = new AttendanceStatus()
            {
                LastCheckInAt = lastAttendance != null ? lastAttendance.CheckInTime : null,
                LastCheckOutAt = lastAttendance != null ? lastAttendance.CheckOutTime : null,
                TotalWorkTime = totalWorkTime,
            };

            return attendenceDetails;
        }

        public async Task<Attendance> MarkCheckIn(long userId)
        {
            var attendance = new Attendance
            {
                UserId = userId,
                CheckInTime = DateTime.UtcNow
            };

            _dataContext.Attendances.Add(attendance);
            return attendance;
        }

        public async Task<Attendance> MarkCheckOut(long userId)
        {
            var attendance = await _dataContext.Attendances
                .Where(a => a.UserId == userId && a.CheckOutTime == null)
                .OrderByDescending(a => a.CheckInTime)
                .FirstOrDefaultAsync();

            if (attendance != null)
            {
                attendance.CheckOutTime = DateTime.UtcNow;
            }
            return attendance;
        }

        public async Task<List<Attendance>> GetAttendances(long userId)
        {
            var attendances = await _dataContext.Attendances.Where(attendence=> attendence.UserId == userId).OrderBy(attendence=> attendence.Id).ToListAsync();
            return attendances;
        }
    }
}
