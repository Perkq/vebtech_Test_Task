using vebtech_Test_Task.Services.Interfaces;
using vebtech_Test_Task.Repositories.Interfaces;
using vebtech_Test_Task.Models;
using vebtech_Test_Task.Exceptions;
using vebtech_Test_Task.DTO;
using AutoMapper;
using System.Net.Mail;

namespace vebtech_Test_Task.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository usRep, IRoleRepository roleRep, IMapper map)
        {
            userRepository = usRep;
            roleRepository = roleRep;
            mapper = map;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await userRepository.GetUserByIdAsync(id) ??
                    throw new NotFoundException("There are no users with id: " + id);

                return mapper.Map<UserDTO>(user);
            } 
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<UserDTO>> GetUsersByNameAsync(string name)
        {
            try
            {
                 var users = await userRepository.GetUsersByNameAsync(name) ??
                    throw new NotFoundException("There are no users with name: " + name);

                return mapper.Map<List<UserDTO>>(users);
            } 
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<UserDTO>> GetUsersByAgeAsync(int age)
        {
            try
            {
                var users = await userRepository.GetUsersByAgeAsync(age) ??
                    throw new NotFoundException("There are no users of this age: " + age);

                return mapper.Map<List<UserDTO>>(users);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await userRepository.GetUserByEmailAsync(email) ??
                    throw new NotFoundException("There are no users with email: " + email);

                return mapper.Map<UserDTO>(user);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await userRepository.GetAllUsersAsync() ??
                    throw new NotFoundException("There are no users in the database");

                return mapper.Map<List<UserDTO>>(users);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<UserDTO> AddNewRoleAsync(int userId, RoleDTO role)
        { 
            try
            {
                User user = userRepository.GetUserByIdAsync(userId).Result ?? 
                    throw new NotFoundException("There are no users in the database");

                var rol = roleRepository.GetRoleByIdAsync(role.RoleId).Result;

                if (user.Roles.Contains(rol))
                {
                    throw new InvalidParametresDataException("User already has this Role");
                }
                return mapper.Map<UserDTO>(await userRepository.AddNewRoleAsync(user, rol));
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO newUser)
        {
            try
            {
                if(!IsEmailValid(newUser.Email) || newUser.Age < 1 || userRepository.GetUserByEmailAsync(newUser.Email) != null)
                {
                    throw new InvalidParametresDataException("Email or age is invalid");
                }

                var userToAdd = new User((await userRepository.GetAllUsersAsync()).Count + 1,newUser.Name, newUser.Age, newUser.Email);

                foreach(var roleUser in newUser.Roles)
                {
                    var role = await roleRepository.GetRoleByIdAsync(roleUser.RoleId);
                    if(role != null)
                    {
                        userToAdd.Roles.Add(role);
                    }
                    else
                    {
                        throw new NotFoundException("Role was not found");
                    }
                }
                
                return mapper.Map<UserDTO>(await userRepository.CreateUserAsync(userToAdd));
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<UserDTO> UpdateUserAsync(int id, UserDTO updatedUser)
        {
            try
            {
                var userId = await userRepository.GetUserByIdAsync(id) ??
                    throw new NotFoundException("There is no user with id: " + id);

                if (updatedUser.GetType().GetFields().All(x => x != null))
                {
                    throw new InvalidParametresDataException("Updated user is not full");
                }

                return mapper.Map<UserDTO>(await userRepository.UpdateUserAsync(userId, userId));
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public async Task<UserDTO> DeleteUserAsync(int id)
        {
            try
            {
                var userId = await userRepository.GetUserByIdAsync(id) ??
                    throw new NotFoundException("There is no user with id: " + id);

                return mapper.Map<UserDTO>(await userRepository.DeleteUserAsync(userId));
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<UserDTO>> GetSortedByNameAsync()
        {
            try
            {
                var userList = await userRepository.GetSortedByNameAsync() ??
                    throw new NotFoundException("User list is empty");

                return mapper.Map<List<UserDTO>>(userList);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<UserDTO>> GetSortedByAgeAsync()
        {
            try
            {
                var userList = await userRepository.GetSortedByAgeAsync() ??
                    throw new NotFoundException("User list is empty");
                
                return mapper.Map<List<UserDTO>>(userList);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<UserDTO>> GetSortedByEmailAsync()
        {
            try
            {
                var userList = await userRepository.GetSortedByEmailAsync() ??
                    throw new NotFoundException("User list is empty");

                return mapper.Map<List<UserDTO>>(userList);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public List<PageUsers> PaginateUsers(List<UserDTO> users, int pageSize)
        {
            var totalItems = users.Count();
            var pageCount = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var paginatedUserGroups = new List<PageUsers>();

            for (int i = 0; i < pageCount; i++)
            {
                var pageUsers = users.Skip(i * pageSize).Take(pageSize).ToList();
                var pageInformation = new PageInformation(i + 1, pageSize, totalItems);
                var pageUser = new PageUsers(pageUsers, pageInformation);
                paginatedUserGroups.Add(pageUser);
            }

            return paginatedUserGroups;
        }


        public bool IsEmailValid(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
