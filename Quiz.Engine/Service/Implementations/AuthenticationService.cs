using Mapster;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quiz.Engine.Data.Entities;
using Quiz.Engine.Data.Repositories.Contracts;
using Quiz.Engine.Service.Contracts;
using Quiz.Engine.Service.DTOs;
using Quiz.Engine.Utils.Settings;
using Quiz.Engine.ViewModels.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Quiz.Engine.Service.Implementations;

public class AuthenticationService : IAuthenticationService
{
    private readonly JWTSettings _jwtSettings;
    private readonly IQuizEngineRepository _quizEngineRepository;

    public AuthenticationService(IOptions<JWTSettings> jwtSettings, IQuizEngineRepository quizEngineRepository)
    {
        _jwtSettings = jwtSettings.Value;
        _quizEngineRepository = quizEngineRepository;
    }

    public async Task RegisterAsync(UserDTO userDTO)
    {
        var existingUserId = await _quizEngineRepository.GetUserAsync(userDTO.Email);

        if (!existingUserId.Equals(Guid.Empty))
        {
            throw new UserAlreadyExistsException();
        }

        var user = userDTO.Adapt<User>();

        var userId = await _quizEngineRepository.CreateUserAsync(user);

        if (userId.Equals(Guid.Empty))
        {
            throw new SomethingWentWrongException();
        }
    }

    public async Task<AuthenticationResponseModel> SignInAsync(UserDTO userDTO)
    {
        var user = userDTO.Adapt<User>();

        var userId = await _quizEngineRepository.GetUserAsync(user);

        if (userId.Equals(Guid.Empty))
        {
            throw new WrongCredentialsException();
        }

        var authentication = GenerateToken(userId);

        return authentication;
    }

    public async Task<AuthenticationResponseModel> SignInByIdAsync(Guid userId)
    {
        var user = await _quizEngineRepository.GetUserByIdAsync(userId);

        if (user is null)
        {
            throw new UserIsNotInTheSystemAnymoreException();
        }

        var authentication = GenerateToken(userId);

        return authentication;
    }

    #region Helper

    private AuthenticationResponseModel GenerateToken(Guid userId)
    {
        var response = new AuthenticationResponseModel()
        {
            AccessToken = GenerateJWTToken(userId, false),
            AccessTokenExpireDate = _jwtSettings.AccessTokenExpireDate,
            RefreshToken = GenerateJWTToken(userId, true),
            RefreshTokenExpireDate = _jwtSettings.RefreshTokenExpireDate
        };

        return response;
    }

    private string GenerateJWTToken(Guid userId, bool isRefreshToken)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.SerialNumber, userId.ToString())
        };

        if (isRefreshToken)
        {
            claims.Add(new Claim(ClaimTypes.AuthorizationDecision, "RefreshToken"));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(isRefreshToken ? _jwtSettings.RefreshTokenExpireDate : _jwtSettings.AccessTokenExpireDate),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    #endregion
}
