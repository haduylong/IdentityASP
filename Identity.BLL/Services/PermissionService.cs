using AutoMapper;
using Identity.Common.DTOs.Request;
using Identity.Common.DTOs.Response;
using Identity.DAL.Entities;
using Identity.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.BLL.Services
{
    public class PermissionService
    {
        private readonly PermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public PermissionService(PermissionRepository permissionRepository, IMapper mapper) 
        { 
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionResponse>> GetAllPermissionAsync()
        {
            var permissions = await _permissionRepository.GetAllAsync();
            var responses = _mapper.Map<IEnumerable<PermissionResponse>>(permissions);
            return responses;
        }

        public async Task<PermissionResponse> CreatePermissionAsync(PermissionRequest request)
        {
            var permission = _mapper.Map<Permission>(request);
            permission = await _permissionRepository.CreateAsync(permission);
            var response = _mapper.Map<PermissionResponse>(permission);
            return response;
        }

        public async Task DeletePermissionAsync(string id)
        {
            await _permissionRepository.DeleteAsync(id);
        }
    }
}
