using wellmanage.shared.Models;

namespace wellmanage.application.Interfaces;

public interface IUserService
{
    Task<BaseResponse> MarkUserCheckIn(long userId);
    Task<BaseResponse> MarkUserCheckOut(long userId);
    Task<AttendanceStatus> GetAttendenceStatus(long userId);
}