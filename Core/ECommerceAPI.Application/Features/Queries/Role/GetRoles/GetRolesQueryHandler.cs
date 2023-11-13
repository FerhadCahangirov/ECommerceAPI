using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Role.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQueryRequest, GetRolesQueryResponse>
    {
        readonly IRoleService _roleService;
        readonly RoleManager<AppRole> _roleManager;

        public GetRolesQueryHandler(IRoleService roleService, RoleManager<AppRole> roleManager)
        {
            _roleService = roleService;
            _roleManager = roleManager;
        }

        public async Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = _roleService.GetAllRoles(request.Page, request.Size);
            return new()
            {
                Datas = datas,
                TotalRoleCount = _roleManager.Roles.Count()
            };
        }
    }
}
