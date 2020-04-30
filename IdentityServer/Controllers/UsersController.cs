using System.Collections.Generic;
using System.Linq;
using IdentityServer.Models.Auth;
using IdentityServer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;

        private readonly UserRepository _context;

        public UsersController(UserRepository context,
            UserManager<ApplicationUser> userMgr)
        {
            _context = context;
            _userManager = userMgr;
        }
        [HttpGet]
        [Route("/users")]
        public IEnumerable<ProfileViewModel> GetUsers()
        {
            var users = _userManager.Users;
            var profiles = ProfileViewModel.GetUserProfiles(_userManager.Users);
            return profiles;
        }

        [HttpGet]
        [Route("/userclaims")]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}