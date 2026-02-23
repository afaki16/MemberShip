using MemberShip.Application.Interfaces;
using AutoMapper;
using MemberShip.Application.Features.Auth.Commands.Login;
using MemberShip.Application.Features.Auth.Commands.Logout;
using MemberShip.Application.Features.Auth.Commands.LogoutAll;
using MemberShip.Application.Features.Auth.Commands.LogoutDevice;
using MemberShip.Application.Features.Auth.Commands.Register;
using MemberShip.Application.Features.Auth.Commands.RefreshToken;
using MemberShip.Application.Features.Auth.Commands.RevokeSession;
using MemberShip.Application.Features.Auth.Commands.ChangePassword;
using MemberShip.Application.Features.Auth.Commands.ForgotPassword;
using MemberShip.Application.Features.Auth.Commands.ResetPassword;
using MemberShip.Domain.Common.Interfaces;
using MemberShip.Domain.Common.Interfaces.Repositories;
using MemberShip.Application.Common.Results;
using MemberShip.Domain.Entities;
using MemberShip.Domain.Common.Enums;
using MemberShip.Domain.Models;
using MemberShip.Application.Features.Users.Dtos;
using MemberShip.Application.Features.Roles.Dtos;
using MemberShip.Application.Features.Tenants.Dtos;
using MemberShip.Application.Features.Permissions.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MemberShip.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IPasswordService passwordService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if email already exists
            if (await _unitOfWork.Users.EmailExistsAsync(request.Email))
             return Result<UserDto>.Failure(Error.Failure(
                  ErrorCode.AlreadyExists,
                  "Email already exists"));

        // Hash password
        var passwordResult = _passwordService.HashPassword(request.Password);
            if (!passwordResult.IsSuccess)
            return Result<UserDto>.Failure(Error.Failure(
            ErrorCode.AlreadyExists,
            $"{passwordResult.Error}"));

        // Create user
        var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordResult.Value,
                PhoneNumber = request.PhoneNumber,
                Status = UserStatus.Active,
                EmailConfirmed = false
            };

            // Assign default User role
            var userRole = await _unitOfWork.Roles.GetByNameAsync("User");
            if (userRole != null)
            {
                user.UserRoles.Add(new UserRole { RoleId = userRole.Id });
            }

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(userDto);
        }
    }
} 
