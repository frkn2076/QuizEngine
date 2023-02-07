using Quiz.Engine.Service.DTOs;
using Quiz.Engine.ViewModels.Responses;

namespace Quiz.Engine.Service.Contracts;

public interface IAuthenticationService
{
    public Task RegisterAsync(UserDTO userDTO);

    public Task<AuthenticationResponseModel> SignInAsync(UserDTO userDTO);

    public Task<AuthenticationResponseModel> SignInByIdAsync(Guid userId);
}
