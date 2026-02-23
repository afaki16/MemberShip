using MemberShip.Application.Features.Users.Dtos;
using MemberShip.Application.Features.Roles.Dtos;
using MemberShip.Application.Features.Tenants.Dtos;
using MemberShip.Application.Features.Permissions.Dtos;
using MemberShip.Application.Common.Results;
using MediatR;
using System;

namespace MemberShip.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Result<UserListDto>>
    {
        public int Id { get; set; }
    }
} 
