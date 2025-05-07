using AutoMapper;
using wellmanage.application.Interfaces;
using wellmanage.data.Interfaces;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage.application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<AttendanceStatus>> MarkUserCheckIn(long userId)
    {
        var response = new ServiceResponse<AttendanceStatus>();
        var attendanceStatus = await _userRepository.GetAttendanceStatus(userId);
        if (attendanceStatus.IsAlreadyCheckedIn)
        {
            response.HasError = true;
            response.ErrorList = new List<string>()
            {
                "User is Already Checked In"
            };
            return response;
        }
        var attendance = await _userRepository.MarkCheckIn(userId);
        await _unitOfWork.SaveChangesAsync();
        attendanceStatus.LastCheckInAt = attendance.CheckInTime;
        attendanceStatus.LastCheckOutAt = null;
        response.ResponseData = attendanceStatus;
        return response;
    }

    public async Task<ServiceResponse<AttendanceStatus>> MarkUserCheckOut(long userId)
    {
        var response = new ServiceResponse<AttendanceStatus>();

        var attendanceStatus = await _userRepository.GetAttendanceStatus(userId);

        if (!attendanceStatus.IsAlreadyCheckedIn || attendanceStatus.IsAlreadyCheckedOut)
        {
            response.HasError = true;
            response.ErrorList = new List<string>()
        {
            attendanceStatus.IsAlreadyCheckedOut ? "User is already checked out" :
            !attendanceStatus.IsAlreadyCheckedIn ? "User is not checked in" :
            "Unknown error"
        };

            return response;
        }

        var attendance = await _userRepository.MarkCheckOut(userId);
        await _unitOfWork.SaveChangesAsync();
        attendanceStatus.LastCheckOutAt = attendance.CheckOutTime;
        response.ResponseData = attendanceStatus;
        response.HasError = false;
        return response;
    }

    public async Task<AttendanceStatus> GetAttendenceStatus(long userId)
    {
        var status = await _userRepository.GetAttendanceStatus(userId);
        return status;
    }

    public async Task<List<AttendanceResponse>> GetAttendences(long userId)
    {
        var attendances = await _userRepository.GetAttendances(userId);
        var attendanceSummary = _mapper.Map<List<AttendanceResponse>>(attendances);
        return attendanceSummary;
    }

}