using AutoMapper;
using LayeredDBFirstDemo.Contracts.Interface;
using LayeredDBFirstDemo.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBFirstLayeredDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IMapper _mapper;

        public UserController(IUserBusiness userBusiness, IMapper autoMapping)
        {
            this._userBusiness = userBusiness;
            this._mapper = autoMapping;
        }

        // Get All Users
        [Route("/api/GetUsers")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _userBusiness.GetAllUsers().ConfigureAwait(false);
            var result = _mapper.Map<List<User>, List<UserDto>>(response);
            return new OkObjectResult(result);
        }

        //Get UserData ByID
        [Route("/api/GetUserByID/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int userId)
        {
            var response = await _userBusiness.GetUserById(userId);
            var result = _mapper.Map<User, UserDto>(response);
            return new OkObjectResult(result);

        }


        // Add User 
        [Route("/api/AddUser")]
        [HttpPost]
        public async Task<ObjectResult> PostAsync(UserDto UserDataDto)
        {
            var userData = _mapper.Map<UserDto, User>(UserDataDto);
            var response = await _userBusiness.AddUser(userData);
            var result = _mapper.Map<User, UserDto>(response);
            return new OkObjectResult(result);


        }

    }
}