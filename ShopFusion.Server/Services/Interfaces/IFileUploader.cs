using Microsoft.AspNetCore.Components.Forms;

namespace ShopFusion.Server.Services.Interfaces
{
	public interface IFileUploader
	{
		Task<string> UploadFile(IBrowserFile file, string folderDirectory, string relativePath);
		bool DeleteFile(string filePath);
	}
}