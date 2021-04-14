using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Services.Interfaces;

namespace Web.Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("user")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUser()
        {
            var results = await _userRepository.GetAllUserAsync();
            return Ok(results);
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
    }
}
