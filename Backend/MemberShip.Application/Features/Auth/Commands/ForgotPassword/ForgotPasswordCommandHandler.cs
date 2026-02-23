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
using MediatR;


namespace MemberShip.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement forgot password logic
            return Result.Success();
        }
    }
} 
