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
using OfficeOpenXml;
using System.Threading;

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


		public static (string, bool, List<ImageOfReport>) UploadImage_Report(List<IFormFile> formFiles, string prefix, string UploadPath, string folderName, int ReportId)
		{
			List<ImageOfReport> images= new List<ImageOfReport>();

			foreach(var formFile in formFiles )
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
						return ("فایل خالی میباشد", false,null);
					}
				}
				catch (Exception ex)
				{
					return ("مشکلی پیش آمده. لطفا مجددا تلاش نمایید", false, null);
				}
				images.Add(new ImageOfReport()
				{
					ReportId = ReportId,
					Url = pathOfFile
				});
			}
			




			return ("", true,images);
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


		public static (string,bool,int) CheckExcel(IFormFile file)
		{
			int total = 0;
			if (file == null || file.Length <= 0)
				return ("فایل خالی میباشد.", false,0);

			if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
				return ("فرمت فایل باید .xlsx باشد.", false,0);

			using (var stream = new MemoryStream())
			{
				file.CopyTo(stream);
				ExcelPackage.LicenseContext = LicenseContext.Commercial;
				using (var package = new ExcelPackage(stream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
					var rowCount = worksheet.Dimension.Rows;
					if(rowCount <= 1)
					{
						return ("شرکت کننده ای وجود ندارد", false,0);
					}
					total = rowCount - 1;
					for(int i = 1; i <= rowCount; i++)
					{
						if((i != rowCount) && (worksheet.Cells[i, 1].Value == null || worksheet.Cells[i, 2].Value == null || worksheet.Cells[i, 3].Value == null || worksheet.Cells[i, 4].Value == null))
						{
							return ($"سطر{i} مقدار خالی دارد", false,0);
						}
						if((rowCount == 2) && (worksheet.Cells[2, 1].Value == null || worksheet.Cells[2, 2].Value == null || worksheet.Cells[2, 3].Value == null || worksheet.Cells[2, 4].Value == null))
						{
							return ("شرکت کننده ای وجود ندارد", false, 0);
						}
					}
				}
			}
			return ("", true,total);
		}

		public static List<ParticipantsOfCertificate> ReadExcel_Certificate(string filePath, int CertificationId)
		{
			var res = new List<ParticipantsOfCertificate>();
			int total = 0;
			//var package = new ExcelPackage(new FileInfo(BasePath));

			using (var stream = new MemoryStream())
			{
				ExcelPackage.LicenseContext = LicenseContext.Commercial;
				var package = new ExcelPackage(new FileInfo(filePath));
				ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
				var rowCount = worksheet.Dimension.Rows;
				for (int i = 2; i <= rowCount; i++)
				{
					if(worksheet.Cells[i, 1].Value != null && worksheet.Cells[i, 2].Value != null && worksheet.Cells[i, 3].Value !=null && worksheet.Cells[i, 4].Value != null)
					{
						res.Add(new ParticipantsOfCertificate()
						{
							FirstName = worksheet.Cells[i, 1].Value.ToString(),
							LastName = worksheet.Cells[i, 2].Value.ToString(),
							Email = worksheet.Cells[i, 3].Value.ToString(),
							Phone = worksheet.Cells[i, 4].Value.ToString(),
							CertificationId = CertificationId
						});
					}
					
				}
			}
			return res;
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
