using AutoMapper;
using Identity.Common.DTOs.Request;
using Identity.Common.DTOs.Response;
using Identity.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Common.Mappers
{
    #region --UserMapper--
    public class UserMapper : Profile
    {
        public UserMapper() 
        {
            CreateMap<UserCreateRequest, User>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
            CreateMap<UserUpdateRequest, User>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<User, UserResponse>();
        }
    }
    #endregion

    #region --PermissionMapper--
    public class PermissionMapper : Profile
    {
        public PermissionMapper()
        {
            CreateMap<PermissionRequest, Permission>();

            CreateMap<Permission, PermissionResponse>();
        }
    }
    #endregion

    #region --RoleMapper--
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<RoleRequest, Role>()
                .ForMember(dest => dest.Permissions, opt => opt.Ignore());

            CreateMap<Role, RoleResponse>();

            CreateMap<Role, RoleResponseBase>();
        }
    }
    #endregion
}
