using MemberShip.Application.Features.Auth.Dtos;
using MemberShip.Application.Common.Results;
using MediatR;
using System.Collections.Generic;

namespace MemberShip.Application.Features.Auth.Queries.GetUserSessions
{
    public class GetUserSessionsQuery : IRequest<Result<IEnumerable<SessionDto>>>
    {
        public int UserId { get; set; }
    }
} 
