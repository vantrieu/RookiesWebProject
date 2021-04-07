using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Interfaces;
using Web.ShareModels.ViewModels;

namespace Web.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserVm> DeleteUserAsync(string Id)
        {
            var result = await (from u in _context.Users
                                where u.Id == Id
                                select new UserVm
                                {
                                    UserId = u.Id,
                                    FullName = u.FullName,
                                    Email = u.Email,
                                    PhoneNumber = u.PhoneNumber,
                                    LockoutEnd = u.LockoutEnd
                                }).FirstOrDefaultAsync();
            if (result == null)
                return null;
            return result;
        }

        public async Task<IEnumerable<UserVm>> GetAllAdminAsync()
        {
            var admins = await (from u in _context.Users
                                 join ur in _context.UserRoles on u.Id equals ur.UserId
                                 join r in _context.Roles on ur.RoleId equals r.Id
                                 where r.Name == "admin"
                                 select new UserVm
                                 {
                                     UserId = u.Id,
                                     FullName = u.FullName,
                                     Email = u.Email,
                                     PhoneNumber = u.PhoneNumber,
                                     LockoutEnd = u.LockoutEnd
                                 }).ToArrayAsync();
            var superAdmins = await (from u in _context.Users
                                 join ur in _context.UserRoles on u.Id equals ur.UserId
                                 join r in _context.Roles on ur.RoleId equals r.Id
                                 where r.Name == "superadmin"
                                 select new UserVm
                                 {
                                     UserId = u.Id,
                                     FullName = u.FullName,
                                     Email = u.Email,
                                     PhoneNumber = u.PhoneNumber,
                                     LockoutEnd = u.LockoutEnd
                                 }).ToArrayAsync();
            return admins.Except(superAdmins); 
        }

        public async Task<IEnumerable<UserVm>> GetAllSuperAdminAsync()
        {
            var results = await (from u in _context.Users
                                join ur in _context.UserRoles on u.Id equals ur.UserId
                                join r in _context.Roles on ur.RoleId equals r.Id
                                where r.Name == "superadmin"
                                select new UserVm
                                {
                                    UserId = u.Id,
                                    FullName = u.FullName,
                                    Email = u.Email,
                                    PhoneNumber = u.PhoneNumber,
                                    LockoutEnd = u.LockoutEnd
                                }).ToArrayAsync();
            return results;
        }

        public async Task<IEnumerable<UserVm>> GetAllUserAsync()
        {
            var users = await (from u in _context.Users
                               join ur in _context.UserRoles on u.Id equals ur.UserId
                               join r in _context.Roles on ur.RoleId equals r.Id
                               where r.Name == "user"
                               select new UserVm
                               {
                                   UserId = u.Id,
                                   FullName = u.FullName,
                                   Email = u.Email,
                                   PhoneNumber = u.PhoneNumber,
                                   LockoutEnd = u.LockoutEnd
                               }).ToListAsync();
            var admins = await (from u in _context.Users
                                join ur in _context.UserRoles on u.Id equals ur.UserId
                                join r in _context.Roles on ur.RoleId equals r.Id
                                where r.Name == "admin"
                                select new UserVm
                                {
                                    UserId = u.Id,
                                    FullName = u.FullName,
                                    Email = u.Email,
                                    PhoneNumber = u.PhoneNumber,
                                    LockoutEnd = u.LockoutEnd
                                }).ToListAsync();
            var results = users.Except(admins);
            return results;
        }

        public async Task<UserVm> GetUserById(string Id)
        {
            var result = await (from u in _context.Users
                                 where u.Id == Id
                                 select new UserVm
                                 {
                                     UserId = u.Id,
                                     FullName = u.FullName,
                                     Email = u.Email,
                                     PhoneNumber = u.PhoneNumber,
                                     LockoutEnd = u.LockoutEnd
                                 }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> LockUser(string Id)
        {
            var result = await (from u in _context.Users where u.Id == Id select u).FirstOrDefaultAsync();
            if (result == null)
                return false;
            result.LockoutEnd = DateTimeOffset.Parse(DateTime.Today.AddDays(365).ToString());
            _context.Update(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnLockUser(string Id)
        {
            var result = await (from u in _context.Users where u.Id == Id select u).FirstOrDefaultAsync();
            if (result == null)
                return false;
            result.LockoutEnd = null;
            _context.Update(result);
            await _context.SaveChangesAsync();
            return true; ;
        }
    }
}
