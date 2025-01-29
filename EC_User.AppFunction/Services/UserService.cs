using AutoMapper;
using EC_User.AppFunction.Models;
using EC_User.Domain.Entities;
using EC_User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EC_User.AppFunction.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User?> CreateUser(UserRegisterPayload payload)
        {
            var userMapping = _mapper.Map<User>(payload);

            var register = await _userRepository.GetUsers()
                                .FirstOrDefaultAsync(u => u.Email == payload.Email);

            if(register != null) return null!;

            var newUser = await _userRepository.CreateUser(userMapping);
            
            return newUser;
        }
        

        public async Task<User?> UpdateUser(User payload)
        {
            var register = await _userRepository.GetUsers()
                                .FirstOrDefaultAsync(u => u.Id == payload.Id);

            if(register == null) return null!;

            var existingEmail = await _userRepository.GetUsers()
                                    .FirstOrDefaultAsync(u => u.Email == payload.Email);

            if(existingEmail != null) return null!;

            var updatedUser = await _userRepository.UpdateUser(payload);
            return updatedUser;
        }

        public async Task<User?> DeleteUser(long id)
        {
            var register = await _userRepository.GetUsers()
                                .FirstOrDefaultAsync(u => u.Id == id);

            if(register == null) return null!;

            var deletedUser = await _userRepository.DeleteUser(register);
            return deletedUser;
        }

        public async Task<List<User>> GetUsers(string search)
        {
            List<User> users;
            if(!string.IsNullOrEmpty(search))
            {
                users = await _userRepository.GetUsers()
                            .Where(u => u.Fullname.Contains(search))
                            .ToListAsync();

                return users;
            }

            users = await _userRepository.GetUsers().ToListAsync();

            return users;
        }
    }
}