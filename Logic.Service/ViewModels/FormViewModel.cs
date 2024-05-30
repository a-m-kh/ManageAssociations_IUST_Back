using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	public class FormViewModel
	{
		[Required(ErrorMessage ="لطفا فرم مد نظر را وارد نمایید.")]
		public IFormFile Form { get; set; }
		public string? Title { get; set; }
	}

	public class UpdateFormViewModel
	{
		[Required(ErrorMessage = "لطفاآیدی فرم مد نظر را وارد نمایید.")]
		public int  Id { get; set; }
		public IFormFile? Form { get; set; }
		public string? Title { get; set; }
	}
}
