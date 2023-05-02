using Microsoft.AspNetCore.Identity;

namespace NoThingStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<IdentityUser> GetUserByIdAsync(string userId);
        Task<IdentityUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);
        Task<IdentityResult> UpdateUserAsync(IdentityUser user);
        Task<IdentityResult> DeleteUserAsync(IdentityUser user);
    }
}
