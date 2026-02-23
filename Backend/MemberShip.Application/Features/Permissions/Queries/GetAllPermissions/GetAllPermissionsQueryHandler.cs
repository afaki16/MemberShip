using AutoMapper;
using MemberShip.Application.Features.Users.Dtos;
using MemberShip.Application.Features.Roles.Dtos;
using MemberShip.Application.Features.Tenants.Dtos;
using MemberShip.Application.Features.Permissions.Dtos;
using MemberShip.Application.Features.Permissions.Queries.GetAllPermissions;
using MemberShip.Domain.Common.Interfaces;
using MemberShip.Domain.Common.Interfaces.Repositories;
using MemberShip.Application.Common.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MemberShip.Application.Features.Permissions.Queries.GetAllPermissions
{
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, Result<IEnumerable<PermissionDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPermissionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PermissionDto>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _unitOfWork.Permissions.GetAllAsync();
            var permissionDtos = _mapper.Map<IEnumerable<PermissionDto>>(permissions);
            return Result<IEnumerable<PermissionDto>>.Success(permissionDtos);
    }
    }
} 
