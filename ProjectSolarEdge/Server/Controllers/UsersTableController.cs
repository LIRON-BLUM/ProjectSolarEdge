using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSolarEdge.Server.Configuration;
using ProjectSolarEdge.Shared.Entities;
using ProjectSolarEdge.Shared.Services.Users;

namespace ProjectSolarEdge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersTableController : ControllerBase
    {

        //properties
        private IUsersRepository _usersRepository;

        public UsersTableController(IUsersRepository _repo)
        {
            _usersRepository = _repo;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            IEnumerable<UsersTable> Users = _usersRepository.GetAllUsers();

            return Ok(Users);
        }

        [HttpGet]
        [Route("GetUserIdByUserName/{UserName}")]
        public IActionResult GetUserIdByUserName(string UserName)
        {
            UsersTable _UserID = _usersRepository.GetUserIdByUserName(UserName);

            if (_UserID == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_UserID);
        }


    }
}
