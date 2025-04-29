using wellmanage.shared.Models;

namespace wellmanage.application.Interfaces;

public interface IUserService
{
    Task<ServiceResponse<AttendanceStatus>> MarkUserCheckIn(long userId);
    Task<ServiceResponse<AttendanceStatus>> MarkUserCheckOut(long userId);
    Task<AttendanceStatus> GetAttendenceStatus(long userId);
}