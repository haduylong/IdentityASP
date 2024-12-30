using AutoMapper;
using Identity.Common.DTOs.Request;
using Identity.Common.DTOs.Response;
using Identity.Common.Mappers;
using Identity.DAL.Entities;
using Identity.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.BLL.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(UserRepository userRepository, RoleRepository roleRepository, IMapper mapper
            , IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUserAsync()
        {
            var users = await _userRepository.GetAllUserAsync();
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }
 
        public async Task<UserResponse> GetUserByIdAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentNullException($"{nameof(id)} is null");
            }
            var user = await _userRepository.GetUserAsync(user => user.UserId == id);

            if (user == null)
            {
                throw new Exception("Bad request");
            }

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> CreateUserAsync(UserCreateRequest request)
        {
            if (await _userRepository.ExistByUsername(request.Username))
            {
                throw new Exception("User đã tồn tại");
            }

            // create new user by mapping from request
            User user = _mapper.Map<User>(request);
            // get all user's role
            var roles = await _roleRepository.GetAllAsync(r => request.Roles.Contains(r.RoleId));
            user.Roles = roles.ToList();
            // encode password
            user.Password = _passwordHasher.HashPassword(user, request.Password);

            user = await _userRepository.CreateAsync(user);
            UserResponse response = _mapper.Map<UserResponse>(user);
            return response;
        }

        public async Task<UserResponse> UpdateUserAsync(Guid userId, UserUpdateRequest request)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user == null) 
            {
                return null;
            }

            _mapper.Map(request, user);
            var roles = await _roleRepository.GetAllAsync(r => request.Roles.Contains(r.RoleId));
            user.Roles = roles.ToList();

            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserResponse>(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
