using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using EasyCash.Application.Contracts;
using EasyCash.Application.Contracts.Handlers;
using EasyCash.Application.Settings;
using EasyCash.Domain;
using EasyCash.Shared;
using EasyCash.Shared.Exceptions;

namespace EasyCash.Application.UseCases.Auth.Login;

internal class LoginCommandHandler : ICommandHandler<LoginCommand, Result<AuthenticateResponseModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppSettings _settings;
    public LoginCommandHandler(IUnitOfWork unitOfWork, AppSettings settings)
    {
        _unitOfWork = unitOfWork;
        _settings = settings;
    }

    public async Task<Result<AuthenticateResponseModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UsersRepository.FindOneAsync(x => x.Email == request.Email && !x.IsDeleted);

        if (user is not null && BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            var token = GenerateJwtToken(user);

            return LoginCommandResult.Success(new()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Token = token
            });
        }        

        throw new EasyCashException("email or password is incorrect");
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_settings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public class AuthenticateResponseModel
{
    public int Id { get; set; }

    public string Token { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}