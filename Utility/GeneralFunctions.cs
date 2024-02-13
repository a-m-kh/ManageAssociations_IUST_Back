using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
	public static class GeneralFunctions
	{
		public static (string, bool) UploadImage(IFormFile formFile, string prefix, string UploadPath, string folderName)
		{
			var randome = new Random();
			var number = randome.Next(1, 1000000000);
			var uniqueFileName = $"{prefix}_{number}_{formFile.FileName}";
			var pathOfFile = $"{folderName}/{uniqueFileName}";
			try
			{
				if (formFile != null)
				{
					using (FileStream filestream = System.IO.File.Create($"{UploadPath}/{pathOfFile}"))
					{
						formFile.CopyTo(filestream);
						filestream.Flush();
					}
				}
				else
				{
					return ("فایل خالی میباشد", false);
				}
			}
			catch (Exception ex)
			{
				return ("مشکلی پیش آمده. لطفا مجددا تلاش نمایید", false);
			}




			return (pathOfFile, true);
		}

	}
}
