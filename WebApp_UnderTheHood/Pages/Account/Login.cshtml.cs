using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebApp_UnderTheHood.Pages.Account
{
	public class LoginModel : PageModel
	{
		[BindProperty]
		public Credential Credential { get; set; } = new Credential();
		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid) return Page();
			//verify the credential
			if (Credential.UserName == "admin" && Credential.Password == "password")
			{
				//Creating a security context
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, "admin"),
					new Claim(ClaimTypes.Email, "admin@mywebsite.com")
				};

				var identity = new ClaimsIdentity(claims, MyCookieAuthScheme.AuthScheme);

				ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

				await HttpContext.SignInAsync(MyCookieAuthScheme.AuthScheme, claimsPrincipal);

				return RedirectToPage("/Index");
			}

			return Page();
		}

	}
	public class Credential
	{
		[Required]
		[Display(Name = "User Name")]
		public string UserName { get; set; } = string.Empty;
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;
	}
}
