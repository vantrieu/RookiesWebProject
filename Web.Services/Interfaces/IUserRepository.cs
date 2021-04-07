using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.ShareModels.ViewModels;

namespace Web.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserVm>> GetAllUserAsync();

        Task<IEnumerable<UserVm>> GetAllAdminAsync();

        Task<IEnumerable<UserVm>> GetAllSuperAdminAsync();

        Task<UserVm> GetUserById(string Id);

        Task<UserVm> DeleteUserAsync(string Id);

        Task<bool> UnLockUser(string Id);

        Task<bool> LockUser(string Id);

    }
}
