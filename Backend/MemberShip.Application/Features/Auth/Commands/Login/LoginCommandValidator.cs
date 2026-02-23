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

namespace MemberShip.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is not valid.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
} 
