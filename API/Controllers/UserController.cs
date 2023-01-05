using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Api.Entities;
using MovieApi.API.Entities;

namespace MovieApi.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController: Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user){
            if(ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser{
                    UserName = user.Name,
                    Email = user.Email
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if(result.Succeeded){
                    ViewBag.Message = "User Created successfully";
                } else {
                    foreach(IdentityError error in result.Errors){
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(UserRole userRole)
        {
            if(ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole(){
                    Name = userRole.RoleName
                });

                if(result.Succeeded){
                    ViewBag.Message = "Role Created Successfully";

                } else {
                    foreach(IdentityError error in result.Errors){
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
        
    }
    
}