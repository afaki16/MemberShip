using FluentValidation;
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

namespace MemberShip.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("Access token is required.");

            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.");
        }
    }
} 
