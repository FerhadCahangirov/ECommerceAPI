using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;
using AppUserClass = ECommerceAPI.Domain.Entities.Identity.AppUser;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            DTOs.Token token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 900);
            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };

        }
    }
}
