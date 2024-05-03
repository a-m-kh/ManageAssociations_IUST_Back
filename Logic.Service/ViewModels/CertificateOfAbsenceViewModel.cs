using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	internal class CertificateOfAbsenceViewModel
	{
	}

	public class CreateCertificateOfAbsenceViewModel
	{
		[Required(ErrorMessage ="لطفا آیدی انجمن مد نظر را وارد نمایید")]
		public int AssoiciationId { get; set; }

		[Required(ErrorMessage ="عنوان گواهی را وارد نمایید")]
		public string Title { get; set; }
		[Required(ErrorMessage ="لطفا نام درس را وارد نمایید")]
		public string CourseName { get; set; }
		[Required(ErrorMessage = "لطفا نام استاد درس را وارد نمایید")]
		public string ProfessorName { get; set; }
		[Required(ErrorMessage = "لطفا دلیل غیبت را وارد نمایید")]
		public string Reason { get; set; }
		[Required(ErrorMessage = "لطفا تاریخ غیبت را وارد نمایید")]
		public DateTime AbsenceDate { get; set; }

		[Required(ErrorMessage ="لطفا اکسل مد نظر را وارد نمایید")]
		public IFormFile ExcelFile { get; set; }
	}

	public class UpdateCertificateOfAbsenceViewModel
	{
		[Required(ErrorMessage ="لطفا آیدی را وارد نمایید")]
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? CourseName { get; set; }
		public string? ProfessorName { get; set; }
		public string? Reason { get; set; }
		public DateTime? AbsenceDate { get; set; }
		public IFormFile? ExcelFile { get; set; }

	}

	public class ChangeStateCertificateOfAbsenceViewModel
	{
		[Required(ErrorMessage = "لطفا آیدی گواهی مد نظر را وارد نمایید")]
		public int CertificationId { get; set; }
		[Required(ErrorMessage = "لطفا آیدی وضعیت مد نظر را وارد نمایید")]
		public int StatusId { get; set; }
	}

}
