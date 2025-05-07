namespace wellmanage.shared.Enums;

public enum VerificationTypeEnum
{
    OTPCode = 1,
    ConfirmationLink = 2
}

public enum AuthTokenResponseTypeEnum
{
    InvalidAccessToken = 1,
    InvalidRefreshToken = 2,
    InvalidDeviceID = 3
}