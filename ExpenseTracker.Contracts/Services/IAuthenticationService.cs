using ExpenseTracker.Shared.Models;

namespace ExpenseTracker.Contracts.Services;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> Authenticate(LoginInfo userLoginInfo);
}
