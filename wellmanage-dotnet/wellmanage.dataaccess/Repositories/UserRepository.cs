using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wellmanage.data.Data;
using wellmanage.data.Interfaces;
using wellmanage.domain.Entity;

namespace wellmanage.data.Repositories
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public async Task MarkCheckIn(int userId)
        {
            var attendance = new Attendance
            {
                UserId = userId,
                CheckInTime = DateTime.UtcNow
            };

            _dataContext.Attendances.Add(attendance);
            await _dataContext.SaveChangesAsync();
        }

        public async Task MarkCheckOut(int userId)
        {
            var attendance = await _dataContext.Attendances
                .Where(a => a.UserId == userId && a.CheckOutTime == null)
                .OrderByDescending(a => a.CheckInTime)
                .FirstOrDefaultAsync();

            if (attendance != null)
            {
                attendance.CheckOutTime = DateTime.UtcNow;
                await _dataContext.SaveChangesAsync();
            }
        }


    }
}
