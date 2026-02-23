using MemberShip.Application.Common.Results;
using MediatR;

namespace MemberShip.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest<Result>
    {
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Reason { get; set; }
    }
} 
