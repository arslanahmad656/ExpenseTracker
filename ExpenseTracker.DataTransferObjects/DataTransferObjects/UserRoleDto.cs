namespace ExpenseTracker.Shared.DataTransferObjects;

public record UserRoleDto(string Username, string PasswordHash, List<RoleDto> Roles);
