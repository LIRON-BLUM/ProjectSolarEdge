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
        [Route("GetUserByID/{UserID}")]
        public IActionResult GetUserByID(int UserID)
        {
            UsersTable _UserID = _usersRepository.GetUserByID(UserID);

            if (_UserID == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_UserID);
        }


        [HttpGet]
        [Route("GetUserIdByUserName/{UserName}")]
        public IActionResult GetUserIdByUserName(string userName)
        {
            UsersTable _UserID = _usersRepository.GetUserIdByUserName(userName);

            if (_UserID == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_UserID);
        }


        

        [HttpGet]
        [Route("GetUserIdByUserPassword/{UserPassword}")]
        public IActionResult GetUserIdByUserPassword(string userPassword)
        {
            UsersTable _UserID = _usersRepository.GetUserIdByUserPassword(userPassword);

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
