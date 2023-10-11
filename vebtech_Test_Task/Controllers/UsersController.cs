using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vebtech_Test_Task.Models;
using vebtech_Test_Task.Services.Interfaces;
using vebtech_Test_Task.DTO;


namespace vebtech_Test_Task.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userServ)
        {
            userService = userServ;
        }

        [HttpGet]
        [Route("get-user/{userId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Returns user with the specified id")]
        [SwaggerResponse(200, Description = "200 OK: User was found")]
        [SwaggerResponse(404, Description = "404 Not Found: User was not found")]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            var result = await userService.GetUserByIdAsync(userId);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-users-by-name")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Returns users with the specified name")]
        [SwaggerResponse(200, Description = "200 OK: Users were found")]
        [SwaggerResponse(404, Description = "404 Not Found: Users were not found")]
        public async Task<IActionResult> GetUserByNameAsync([FromBody] string name)
        {
            var result = await userService.GetUsersByNameAsync(name);
            return Ok(result);
        }

        [HttpPost]
        [Route("get-user-by-age")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Returns users with the specified age")]
        [SwaggerResponse(200, Description = "200 OK: Users were found")]
        [SwaggerResponse(404, Description = "404 Not Found: Users were not found")]
        public async Task<IActionResult> GetUserByAgeAsync([FromBody] int age)
        {
            var result = await userService.GetUsersByAgeAsync(age);
            return Ok(result);
        }

        [HttpPost]
        [Route("get-user-by-email")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Returns user with the specified email")]
        [SwaggerResponse(200, Description = "200 OK: User was found")]
        [SwaggerResponse(404, Description = "404 Not Found: User was not found")]
        public async Task<IActionResult> GetUserByEmailAsync([FromBody] string email)
        {
            var result = await userService.GetUserByEmailAsync(email);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-all-users")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Returns users list")]
        [SwaggerResponse(200, Description = "200 OK: Users were got")]
        [SwaggerResponse(404, Description = "404 Not Found: Users were not found")]
        public async Task<IActionResult> GetAllUsersByIdAsync()
        {
            var result = await userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpPut]
        [Route("add-new-role-to-user/{userId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Add user new role")]
        [SwaggerResponse(200, Description = "200 OK: role was added")]
        [SwaggerResponse(404, Description = "404 Not Found: User was not found")]
        [SwaggerResponse(409, Description = "409 Invalid parametres: Such role does not exist")]
        public async Task<IActionResult> AddNewRoleAsync(int userId, [FromBody] RoleDTO role)
        {
            var result = await userService.AddNewRoleAsync(userId, role);
            return Ok(result);
        }

        [HttpPut]
        [Route("create-new-user")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Add user new role")]
        [SwaggerResponse(200, Description = "200 OK: role was added")]
        [SwaggerResponse(409, Description = "409 Invalid parametres: user data is invalid")]
        public async Task<ActionResult<User>> CreateUserAsync([FromBody] UserDTO user)
        {
            var newUser = await userService.CreateUserAsync(user);
            return Ok(newUser);
        }

        [HttpPut]
        [Route("update-user/{userId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Add user new role")]
        [SwaggerResponse(200, Description = "200 OK: role was added")]
        [SwaggerResponse(404, Description = "404 Not Found: User was not found")]
        [SwaggerResponse(409, Description = "409 Invalid parametres: New user data is invalid")]
        public async Task<IActionResult> UpdateUserAsync(int userId, [FromBody] UserDTO updatedUser)
        {
            var result = await userService.UpdateUserAsync(userId, updatedUser);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-user/{userId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Add user new role")]
        [SwaggerResponse(200, Description = "200 OK: role was added")]
        [SwaggerResponse(404, Description = "404 Not Found: User was not found")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            var result = await userService.DeleteUserAsync(userId);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-sorted-users-by-name")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Returns users list sorted by name")]
        [SwaggerResponse(200, Description = "200 OK: Users were got")]
        [SwaggerResponse(404, Description = "404 Not Found: Users were not found")]
        public async Task<IActionResult> GetSortedUsersByNameAsync()
        {
            var result = await userService.GetSortedByNameAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("get-sorted-users-by-age")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Returns users list sorted by age")]
        [SwaggerResponse(200, Description = "200 OK: Users were got")]
        [SwaggerResponse(404, Description = "404 Not Found: Users were not found")]
        public async Task<IActionResult> GetSortedUsersByAgeAsync()
        {
            var result = await userService.GetSortedByAgeAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("get-sorted-users-by-email")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Returns users list sorted by email")]
        [SwaggerResponse(200, Description = "200 OK: Users were got")]
        [SwaggerResponse(404, Description = "404 Not Found: Users were not found")]
        public async Task<IActionResult> GetSortedUsersByEmailAsync()
        {
            var result = await userService.GetSortedByEmailAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("get-page/{pagesize}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Returns one page")]
        public async Task<IActionResult> PaginateUsers( int pagesize)
        {
            var result = userService.PaginateUsers(await userService.GetAllUsersAsync(), pagesize);
            return Ok(result);
        }
    }
}
