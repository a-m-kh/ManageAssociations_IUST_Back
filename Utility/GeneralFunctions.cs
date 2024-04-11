using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories.Interface;
using Microsoft.AspNetCore.Identity;

namespace Utility
{
	public static class GeneralFunctions
	{
		public static (string, bool) Upload(IFormFile formFile, string prefix, string UploadPath, string folderName)
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

		public static void DeleteImage(string url)
		{
			try
			{
				System.IO.File.Delete(url);
			}catch(Exception ex)
			{

			}
		}


		public static (bool,string) CheckPermission(int associationId , User user, UserManager<User> userManager, IAssociationRepository associationRepository )
		{

			var association = associationRepository.Get(associationId);

			if(association == null)
			{
				return (false, "همچین انجمنی وجود ندارد");
			}
			var roles = userManager.GetRolesAsync(user).Result.ToList();

			if ((association == null || user.Id != association.AdminId) &&
			    roles.Find(a => a == "SuperAdmin") == null)
			{
				return(false, "شما به این انجمن دسترسی ندارید.");
			}
			return (true, "");
		}

		public static (bool, string) CheckPermissionWithAssociation(AssociationViewDto association, User user, UserManager<User> userManager)
		{
			var roles = userManager.GetRolesAsync(user).Result.ToList();

			if ((association == null || user.Id != association.AdminId) &&
			    roles.Find(a => a == "SuperAdmin") == null)
			{
				return (false, "شما به این انجمن دسترسی ندارید.");
			}
			return (true, "");
		}
	}
}
