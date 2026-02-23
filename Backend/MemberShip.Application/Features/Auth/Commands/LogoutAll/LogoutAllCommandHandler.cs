using MemberShip.Application.Interfaces;
using MemberShip.Application.Features.Auth.Commands.Login;
using MemberShip.Application.Features.Auth.Commands.Logout;
using MemberShip.Application.Features.Auth.Commands.LogoutAll;
using MemberShip.Application.Features.Auth.Commands.LogoutDevice;
using MemberShip.Application.Features.Auth.Commands.Register;
using MemberShip.Application.Features.Auth.Commands.RefreshToken;
using MemberShip.Application.Features.Auth.Commands.RevokeSession;
using MemberShip.Application.Features.Auth.Commands.ChangePassword;
using MemberShip.Application.Features.Auth.Commands.ForgotPassword;
using MemberShip.Application.Features.Auth.Commands.ResetPassword;
using MemberShip.Domain.Common.Interfaces;
using MemberShip.Domain.Common.Interfaces.Repositories;
using MemberShip.Application.Common.Results;
using MemberShip.Domain.Common.Enums;
using MemberShip.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MemberShip.Application.Features.Auth.Commands.LogoutAll
{
    public class LogoutAllCommandHandler : IRequestHandler<LogoutAllCommand, Result>
    {
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUserService;

        public LogoutAllCommandHandler(IAuthService authService, ICurrentUserService currentUserService)
        {
            _authService = authService;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(LogoutAllCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (!userId.HasValue)
            return Result<int>.Failure(Error.Failure(
               ErrorCode.NotFound,
               "User not authenticated"));

        return await _authService.RevokeAllUserTokensAsync(userId.Value, request.IpAddress, request.UserAgent, request.Reason);
        }
    }
} 
