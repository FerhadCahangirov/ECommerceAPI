using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using AppUserClass = ECommerceAPI.Domain.Entities.Identity.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using ECommerceAPI.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUserClass> _userManager;
        private readonly IEndpointReadRepository _endpointReadRepository;

        public UserService(UserManager<AppUserClass> userManager, IEndpointReadRepository endpointReadRepository)
        {
            _userManager = userManager;
            _endpointReadRepository = endpointReadRepository;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.Fullname
            }, model.Password);

            CreateUserResponse response = new CreateUserResponse() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "User added succcessfully";
            else
                foreach (IdentityError error in result.Errors)
                    response.Message += $"{error.Code}-{error.Description} <br>";
            return response;
        }

        
        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUserClass user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else throw new NotFoundUserException();
            
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUserClass user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                resetToken = resetToken.UrlDecode();

                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();
            }
        }

        public async Task<List<ListUser>> GetAllUsersAsync(int page, int size)
        {
            List<AppUser>? users = await _userManager.Users.Skip(page * size).Take(size).ToListAsync();

            return users.Select(user => new ListUser { 
                Id = user.Id,
                Email = user.Email,
                NameSurname = user.NameSurname,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName,
            }).ToList();
        }

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            AppUserClass user = await _userManager.FindByIdAsync(userId);
            if(user != null )
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUserClass user = await _userManager.FindByIdAsync(userIdOrName);

            user ??= await _userManager.FindByNameAsync(userIdOrName);

            if (user != null)
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            return Array.Empty<string>();
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);
            if (!userRoles.Any())
                return false;

            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code);

            if(endpoint == null)
                return false;

            var hasRole = false;
            var endpointRoles = endpoint.Roles.Select(r => r.Name);

            foreach (var userRole in userRoles)
            {
                if (!hasRole)
                {
                    foreach (var endpointsRole in endpointRoles)
                    {
                        if(userRole == endpointsRole)
                        {
                            hasRole = true;
                            break;
                        }
                    }
                }
                else break;
            }

            return hasRole;
        }

        public int TotalUsersCount => _userManager.Users.Count();
    }
}
