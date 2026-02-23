using MemberShip.Application.Common.Results;
using MediatR;

namespace MemberShip.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Result>
    {
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
    }
} 
