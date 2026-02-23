using MemberShip.Application.Features.Users.Dtos;
using MemberShip.Application.Features.Roles.Dtos;
using MemberShip.Application.Features.Tenants.Dtos;
using MemberShip.Application.Features.Permissions.Dtos;
using MemberShip.Application.Common.Results;
using MediatR;
using System.Collections.Generic;

namespace MemberShip.Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<Result<IEnumerable<RoleDto>>>
    {
    }
} 
