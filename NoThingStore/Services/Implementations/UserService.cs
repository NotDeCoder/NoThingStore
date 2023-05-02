using Microsoft.AspNetCore.Identity;
using NoThingStore.Data.Repositories.Interfaces;
using NoThingStore.Services.Interfaces;

namespace NoThingStore.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<IdentityUser> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
        {
            return await _userRepository.CreateUserAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUserAsync(IdentityUser user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(IdentityUser user)
        {
            return await _userRepository.DeleteUserAsync(user);
        }
    }
}
