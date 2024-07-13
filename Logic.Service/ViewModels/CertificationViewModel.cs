using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	internal class CertificationViewModel
	{
	}

	public class CertificationViewModelBase
	{
		[Required(ErrorMessage ="لطفا عنوان گواهی را وارد نمایید")]
		public virtual string Title { get;set; }
		public virtual string Organizer { get; set; }
		[Required(ErrorMessage = "لطفا فایل شرکت کننده ها را وارد نمایید")]
		public virtual IFormFile File { get; set; }
	}

	public class CreateCertificationViewModel : CertificationViewModelBase
	{
		[Required(ErrorMessage ="لطفا آیدی انجمن را وارد نمایید")]
		public int AssociationId { get; set; }
		public int DayCount { get; set; }
		public string Tarikh { get; set; }
	}

	public class UpdateCertificationViewModel 
	{
		[Required(ErrorMessage ="لطفا آیدی گواهی مد نظر را وارد نمایید")]
		public int Id { get; set; }
		/*[Required(ErrorMessage ="لطفا آیدی گواهی مد نظر را وارد نمایید")]
		public int CertificationId { get; set; }*/
		public  string? Title { get; set; }
		public  string? Organizer { get; set; }
		public  IFormFile? File { get; set; }
		public int? DayCount { get; set; }
		public string? Tarikh { get; set; }
	}


	public class ChangeStateViewModel
	{
		[Required(ErrorMessage = "لطفا آیدی گواهی مد نظر را وارد نمایید")]
		public int CertificationId { get; set; }
		[Required(ErrorMessage = "لطفا آیدی وضعیت مد نظر را وارد نمایید")]
		public int StatusId { get; set; }
	}

}
