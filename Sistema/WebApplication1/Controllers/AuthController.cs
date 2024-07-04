using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using app.Data;
using app.DTO.UserDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthDbContext _authDbContext;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AuthDbContext authDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authDbContext = authDbContext;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
        {
            try
            {
                var newUser = new IdentityUser { UserName = user.UserName, Email = user.Email };
                var result = await _userManager.CreateAsync(newUser, user.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                if (!string.IsNullOrEmpty(user.RoleName))
                {
                    await _userManager.AddToRoleAsync(newUser, user.RoleName);
                }

                return Ok(new { Message = "Usuário cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> AuthUser([FromBody] UserLoginDTO user)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.FindByEmailAsync(user.Email);
                    return Ok(new { User = appUser, Message = "Usuário autenticado com sucesso!" });
                }
                else
                {
                    return BadRequest(new { Message = "Usuário ou senha incorretos!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDTO user)
        {
            try
            {
                var token = ExtractAuthToken();
                var userId = ValidateJwtToken(token);
                var appUser = await _userManager.FindByIdAsync(userId);
                if (appUser == null)
                {
                    return BadRequest(new { Message = "Usuário não autenticado!" });
                }

                var result = await _userManager.ChangePasswordAsync(appUser, user.CurrentPassword, user.NewPassword);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(new { Message = "Senha alterada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
            try
            {
                var token = ExtractAuthToken();
                var userId = ValidateJwtToken(token);
                var appUser = await _userManager.FindByIdAsync(userId);
                if (appUser == null)
                {
                    return BadRequest(new { Message = "Usuário não autenticado!" });
                }

                appUser.Email = user.Email;
                appUser.PhoneNumber = user.PhoneNumber;

                var result = await _userManager.UpdateAsync(appUser);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(new { Message = "Usuário atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var token = ExtractAuthToken();
                var userId = ValidateJwtToken(token);
                var appUser = await _userManager.FindByIdAsync(userId);
                if (appUser == null)
                {
                    return BadRequest(new { Message = "Usuário não autenticado!" });
                }

                var users = await _authDbContext.Users.ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateRoles")]
        public async Task<IActionResult> CreateRoles([FromBody] UserRolesDTO userRolesDTO)
        {
            try
            {
                var token = ExtractAuthToken();
                var userId = ValidateJwtToken(token);
                var appUser = await _userManager.FindByIdAsync(userId);
                if (appUser == null)
                {
                    return BadRequest(new { Message = "Usuário não autenticado!" });
                }

                if (!await _roleManager.RoleExistsAsync(userRolesDTO.Name))
                {
                    var role = new IdentityRole
                    {
                        Name = userRolesDTO.Name
                    };
                    var result = await _roleManager.CreateAsync(role);

                    if (!result.Succeeded)
                    {
                        return BadRequest(result.Errors);
                    }
                }

                return Ok(new { Message = "Perfil cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var token = ExtractAuthToken();
                var userId = ValidateJwtToken(token);
                var appUser = await _userManager.FindByIdAsync(userId);
                if (appUser == null)
                {
                    return BadRequest(new { Message = "Usuário não autenticado!" });
                }

                var roles = await _roleManager.Roles.ToListAsync();

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string ExtractAuthToken()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            return token ?? "";
        }

        private string ValidateJwtToken(string token)
        {
            // Implemente a lógica de validação de token JWT aqui
            return "user_id_from_token";
        }
    }
}
