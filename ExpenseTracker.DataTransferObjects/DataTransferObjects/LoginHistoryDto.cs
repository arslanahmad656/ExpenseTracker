namespace ExpenseTracker.Shared.DataTransferObjects;

public record LoginHistoryDto(int Id, DateTimeOffset? LoginTime, DateTimeOffset? LogoutTime, string IPAddress, RoleDto Role, PrincipalDto Principal);
