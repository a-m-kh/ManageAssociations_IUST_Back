using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	public class SliderImageViewModel
	{

	}

	public class CreateSliderImageViewModel
	{
		[Required(ErrorMessage ="لطفا عکس مد نظر را وارد نمایید.")]
		public IFormFile Image { get; set; }
	}

	public class GetForUserSliderImageResponse
	{
		public string ImageUrl { get; set; }
	}
}
