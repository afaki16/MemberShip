using MemberShip.Application.Common.Results;
using MediatR;
using System;

namespace MemberShip.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
} 
