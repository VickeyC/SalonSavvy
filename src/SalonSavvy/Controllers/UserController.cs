using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalonSavvy.Services;
using SalonSavvy.Models;
using SalonSavvy.Data;

namespace SalonSavvy.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private _userService _uService;
        public UserController(_userService us) {
            _uService = us;
        }

        [HttpGet]
        public IList<UserDTO> GetAllTechUsers() {

            return _uService.GetAllTechUsers();
        }
    }
}
