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
using MemberShip.Domain.Common.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MemberShip.Domain.Models;

namespace MemberShip.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UserListDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserWithRolesAsync(request.Id);
            
            if (user == null)
            return Result<UserListDto>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "User not found"));

        var userDto = _mapper.Map<UserListDto>(user);
            return Result<UserListDto>.Success(userDto);
        }
    }
} 
