using Microsoft.AspNetCore.Identity;

namespace ShopFusion.Models.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}