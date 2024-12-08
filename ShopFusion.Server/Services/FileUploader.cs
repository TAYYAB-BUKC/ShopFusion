using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using ShopFusion.Server.Services.Interfaces;

namespace ShopFusion.Server.Services
{
	public class FileUploader : IFileUploader
	{
		public IWebHostEnvironment _webHostEnvironment { get; set; }
		public FileUploader(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		public bool DeleteFile(string filePath)
		{
			try
			{
				if (File.Exists(_webHostEnvironment + filePath))
				{
					File.Delete(_webHostEnvironment + filePath);
					return true;
				}
			}
			catch (Exception ex)
			{

			}
			return false;
		}

		public async Task<string> UploadFile(IBrowserFile file, string folderDirectory, string relativePath)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(file.Name);
				var newFileName = Guid.NewGuid().ToString("N") + fileInfo.Extension;
				//var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\images\\products";

				if (!Directory.Exists(folderDirectory)) 
				{
					Directory.CreateDirectory(folderDirectory);
				}

				var fullPath = Path.Combine(folderDirectory, newFileName);

				await using FileStream fileStream = new FileStream(fullPath, FileMode.CreateNew);
				await file.OpenReadStream().CopyToAsync(fileStream);

				//return $"/images/products/{newFileName}";
				return $"{relativePath}{newFileName}";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
	}
}
