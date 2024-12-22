using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopFusion.Common;
using ShopFusion.DataAccess.Data;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.Entities;

namespace ShopFusion.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}

		[HttpPost]
		public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO signUpRequestDTO) 
		{
			if (!ModelState.IsValid || signUpRequestDTO == null)
			{
				return BadRequest();
			}

			var user = new ApplicationUser()
			{
				Name = signUpRequestDTO.Name,
				Email = signUpRequestDTO.Email,
				PhoneNumber = signUpRequestDTO.PhoneNumber,
				UserName = signUpRequestDTO.Email,
				EmailConfirmed = true,
			};

			var userResult = await _userManager.CreateAsync(user, signUpRequestDTO.Password);

			if (!userResult.Succeeded)
			{
				return BadRequest(new SignUpReponseDTO()
				{
					IsSuccessful = false,
					Errors = userResult.Errors.Select(e => e.Description)
				});
			}

			var roleResult = await _userManager.AddToRoleAsync(user, CommonConfiguration.Role_Customer);
			
			if (!roleResult.Succeeded)
			{
				return BadRequest(new SignUpReponseDTO()
				{
					IsSuccessful = false,
					Errors = roleResult.Errors.Select(e => e.Description)
				});
			}

			return StatusCode(201);
		}

		[HttpPost]
		public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequestDTO)
		{
			if (!ModelState.IsValid || signInRequestDTO == null)
			{
				return BadRequest();
			}

			var signInResult = await _signInManager.PasswordSignInAsync(signInRequestDTO.Email, signInRequestDTO.Password, false, false);

			if (signInResult.Succeeded)
			{
				var userResult = await _userManager.FindByEmailAsync(signInRequestDTO.Email);

				if (userResult == null)
				{
					return BadRequest(new SignInResponseDTO()
					{
						IsSuccessful = false,
						ErrorMessage = "User not found"
					});
				}

				//Everything is OK and we need to login the user
				return Ok(userResult);
			}
			else
			{
				return Unauthorized(new SignInResponseDTO()
				{
					IsSuccessful = false,
					ErrorMessage = "Invalid Email and Password"
				});
			}
		}
	}
}