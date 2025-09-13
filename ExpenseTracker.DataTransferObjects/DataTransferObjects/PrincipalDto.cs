namespace ExpenseTracker.Shared.DataTransferObjects;

public record PrincipalDto(int Id, string Username, string PasswordHash, bool IsActive, List<RoleDto> Roles);
