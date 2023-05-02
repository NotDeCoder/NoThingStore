using Microsoft.AspNetCore.Identity;

namespace NoThingStore.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<IdentityUser> GetUserByIdAsync(string userId);
        Task<IdentityUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);
        Task<IdentityResult> UpdateUserAsync(IdentityUser user);
        Task<IdentityResult> DeleteUserAsync(IdentityUser user);
    }
}
