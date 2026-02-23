using MemberShip.Application.Common.Results;
using MediatR;

namespace MemberShip.Application.Features.Tenants.Commands.DeleteTenant;

    public class DeleteTenantCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
}
