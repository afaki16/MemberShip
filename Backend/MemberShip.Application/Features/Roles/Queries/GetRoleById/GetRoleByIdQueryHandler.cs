using AutoMapper;
using MemberShip.Application.Features.Roles.Queries.GetAllRoles;
using MemberShip.Application.Features.Roles.Queries.GetRoleById;
using MemberShip.Domain.Common.Interfaces;
using MemberShip.Domain.Common.Interfaces.Repositories;
using MemberShip.Application.Common.Results;
using MemberShip.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MemberShip.Application.Features.Users.Dtos;
using MemberShip.Application.Features.Roles.Dtos;
using MemberShip.Application.Features.Tenants.Dtos;
using MemberShip.Application.Features.Permissions.Dtos;
using MemberShip.Domain.Common.Enums;

namespace MemberShip.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Roles.GetRoleWithPermissionsAsync(request.Id);
            
            if (role == null)
            return Result<RoleDto>.Failure(Error.Failure(
                       ErrorCode.NotFound,
                       "Role not found"));

        var roleDto = _mapper.Map<RoleDto>(role);
            return Result<RoleDto>.Success(roleDto);
    }
    }
} 
