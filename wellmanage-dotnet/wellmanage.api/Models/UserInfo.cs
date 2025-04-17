using wellmanage.shared.Enums;

namespace wellmanage.api.Models;

public class UserBasicInfo
{
    public long Id { get; set; }
    public string UserName { get; set; }
}

public class UserInfo : UserBasicInfo
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public StatusEnum Status { get; set; }
    public string StatusDetail => Status.ToString();
}

public class UserRoleInfo : UserInfo
{
    public string[] RoleNames { get; set; }
}

public class AuthenticatedUser : UserRoleInfo
{
    public string AuthenticationToken { get; set; }
    public string RefreshToken { get; set; }
}