using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;

namespace ShopFusion.Server.Services.Interfaces
{
	public interface IDbInitializer
	{
		void Initialize();
	}
}