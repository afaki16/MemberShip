using MemberShip.Application.Features.Users.Dtos;
using MemberShip.Application.Features.Roles.Dtos;
using MemberShip.Application.Features.Tenants.Dtos;
using MemberShip.Application.Features.Permissions.Dtos;
using MemberShip.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace MemberShip.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<Result<RoleDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> PermissionIds { get; set; } = new List<int>();
    }
} 
