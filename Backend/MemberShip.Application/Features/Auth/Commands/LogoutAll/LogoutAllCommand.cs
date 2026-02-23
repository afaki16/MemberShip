using MemberShip.Application.Common.Results;
using MediatR;

namespace MemberShip.Application.Features.Auth.Commands.LogoutAll
{
    public class LogoutAllCommand : IRequest<Result>
    {
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Reason { get; set; }
    }
} 
