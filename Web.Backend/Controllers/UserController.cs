using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Services.Interfaces;
using Web.ShareModels;
using Web.ShareModels.ViewModels;

namespace Web.Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserController(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("user")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUser()
        {
            var lstUsers = new List<UserReponseVM>();
            var results = await _userRepository.GetAllUserAsync();
            foreach (var user in results)
            {
                var isLock = await _userManager.FindByIdAsync(user.UserId);
                lstUsers.Add(new UserReponseVM
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    UserId = user.UserId,
                    LockoutEnd = isLock.LockoutEnabled
                });
            }
            return Ok(lstUsers);
        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllAdmin()
        {
            var results = await _userRepository.GetAllAdminAsync();
            return Ok(results);
        }

        [HttpGet]
        [Route("superadmin")]
        [Authorize(Roles = "superadmin")]
        public async Task<IActionResult> GetAllSuperAdmin()
        {
            var results = await _userRepository.GetAllSuperAdminAsync();
            return Ok(results);
        }

        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> GetUserProfile()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst("sub").Value;
            var result = await _userRepository.GetUserById(userId);
            return Ok(result);
        }

        [HttpPost]
        [Route("lock/{id}")]
        public async Task<IActionResult> LockUserById(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                await _userManager.SetLockoutEnabledAsync(user, true);
                var lockTime = DateTime.Today.AddDays(365);
                await _userManager.SetLockoutEndDateAsync(user, lockTime);
                return Ok();
            }
            catch
            {
                return NoContent();
            }

        }

        [HttpPost]
        [Route("unlock/{id}")]
        public async Task<IActionResult> UnLockUserById(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                await _userManager.SetLockoutEnabledAsync(user, false);
                await _userManager.SetLockoutEndDateAsync(user, DateTime.Today - TimeSpan.FromDays(1));
                return Ok();
            }
            catch
            {
                return NoContent();
            }

        }
    }
}
