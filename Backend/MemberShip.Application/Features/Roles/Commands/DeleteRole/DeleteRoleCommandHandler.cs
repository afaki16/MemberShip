using MemberShip.Application.Features.Roles.Commands.CreateRole;
using MemberShip.Application.Features.Roles.Commands.UpdateRole;
using MemberShip.Application.Features.Roles.Commands.DeleteRole;
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

namespace MemberShip.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(request.Id);
            
            if (role == null)
            return Result<RoleDto>.Failure(Error.Failure(
              ErrorCode.NotFound,
              "Role not found"));

        if (role.IsSystemRole)
            return Result<RoleDto>.Failure(Error.Failure(
          ErrorCode.InvalidOperation,
          "Cannot modify system roles"));

        _unitOfWork.Roles.SoftDelete(role);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
} 
