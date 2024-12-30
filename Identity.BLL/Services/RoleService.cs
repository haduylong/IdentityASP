using AutoMapper;
using Identity.Common.DTOs.Request;
using Identity.Common.DTOs.Response;
using Identity.DAL.Entities;
using Identity.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.BLL.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepository;
        private readonly PermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public RoleService(RoleRepository roleRepository, PermissionRepository permissionRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleResponse>> GetAllRoleAsync()
        {
            var roles = await _roleRepository.GetAllRoleAsync();
            var responses = _mapper.Map<IEnumerable<RoleResponse>>(roles);
            return responses;
        }

        public async Task<RoleResponse> CreateRoleAsync(RoleRequest request)
        {
            var role = _mapper.Map<Role>(request);
            var permissions = await _permissionRepository.GetAllAsync(p => request.Permissions
                                                                            .Contains(p.PermissionId));

            role.Permissions = permissions.ToList();
            await _roleRepository.CreateAsync(role);

            var response = _mapper.Map<RoleResponse>(role);
            return response;
        }

        public async Task DeleteRoleAsync(string id)
        {
            await _roleRepository.DeleteAsync(id);
        }
    }
}
