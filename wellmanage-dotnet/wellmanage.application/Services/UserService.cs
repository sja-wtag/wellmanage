using wellmanage.application.Interfaces;
using wellmanage.data.Interfaces;
using wellmanage.shared.Models;

namespace wellmanage.application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<BaseResponse> MarkUserCheckIn(long userId)
    {
        var response = new BaseResponse();
        var isAlreadyCheckedIn = await _userRepository.IsAlreadyCheckedIn(userId);
        if (isAlreadyCheckedIn)
        {
            response.HasError = true;
            response.ErrorList = new List<string>()
            {
                "User is Already Checked In"
            };
            return response;
        }
        await _userRepository.MarkCheckIn(userId);
        await _unitOfWork.SaveChangesAsync();
        return response;
    }

    public async Task<BaseResponse> MarkUserCheckOut(long userId)
    {
        var response = new BaseResponse();

        var (isAlreadyCheckedIn, isAlreadyCheckedOut) = await _userRepository.CheckAttendanceStatus(userId);

        if (!isAlreadyCheckedIn || isAlreadyCheckedOut)
        {
            response.HasError = true;
            response.ErrorList = new List<string>()
        {
            isAlreadyCheckedOut ? "User is already checked out" :
            !isAlreadyCheckedIn ? "User is not checked in" :
            "Unknown error"
        };

            return response;
        }

        await _userRepository.MarkCheckOut(userId);
        await _unitOfWork.SaveChangesAsync();


        response.HasError = false;
        return response;
    }

    public async Task<AttendanceStatus> GetAttendenceStatus(long userId)
    {
        var status = await _userRepository.GetAttendanceStatus(userId);
        return status;
    }

}