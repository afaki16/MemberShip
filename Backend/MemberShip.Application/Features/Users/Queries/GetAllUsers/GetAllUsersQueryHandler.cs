using AutoMapper;
using MemberShip.Application.Features.Users.Dtos;
using MemberShip.Application.Features.Roles.Dtos;
using MemberShip.Application.Features.Tenants.Dtos;
using MemberShip.Application.Features.Permissions.Dtos;
using MemberShip.Application.Features.Users.Queries.GetAllUsers;
using MemberShip.Application.Features.Users.Queries.GetUserById;
using MemberShip.Domain.Common.Interfaces;
using MemberShip.Domain.Common.Interfaces.Repositories;
using MemberShip.Application.Common.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MemberShip.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserListDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Users.GetUsersWithRolesAsync(request.Page, request.PageSize, request.SearchTerm);
            var userDtos = _mapper.Map<IEnumerable<Application.Features.Users.Dtos.UserListDto>>(users);
             return Result<IEnumerable<UserListDto>>.Success(userDtos);
    }
    }
} 
