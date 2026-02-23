using MemberShip.Application.Common.Results;
using MemberShip.Application.Features.Tenants.Dtos;
using MediatR;
namespace MemberShip.Application.Features.Tenants.Queries.GetTenantById
{
    public class GetTenantByIdQuery : IRequest<Result<TenantDto>>
    {
        public int Id { get; set; }
    }
}
