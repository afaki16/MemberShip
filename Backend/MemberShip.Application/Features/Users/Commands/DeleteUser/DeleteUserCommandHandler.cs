using MemberShip.Application.Features.Users.Commands.CreateUser;
using MemberShip.Application.Features.Users.Commands.UpdateUser;
using MemberShip.Application.Features.Users.Commands.DeleteUser;
using MemberShip.Domain.Common.Interfaces;
using MemberShip.Domain.Common.Interfaces.Repositories;
using MemberShip.Application.Common.Results;
using MemberShip.Application.Features.Users.Dtos;
using MemberShip.Application.Features.Roles.Dtos;
using MemberShip.Application.Features.Tenants.Dtos;
using MemberShip.Application.Features.Permissions.Dtos;
using MemberShip.Domain.Common.Enums;
using MemberShip.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MemberShip.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.Id);
            
            if (user == null)
            return Result<UserListDto>.Failure(Error.Failure(
              ErrorCode.NotFound,
              "User not found"));

        _unitOfWork.Users.SoftDelete(user);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
} 
