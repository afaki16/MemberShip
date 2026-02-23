using MemberShip.Application.Interfaces;
using MemberShip.Application.Features.Auth.Dtos;
using MemberShip.Application.Common.Results;
using MediatR;

namespace MemberShip.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request.Email, request.Password, request.IpAddress, request.UserAgent, request.DeviceId, request.DeviceName);
        }
    }
} 
