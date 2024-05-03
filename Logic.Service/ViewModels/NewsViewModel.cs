using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	public class NewsViewModel
	{
	}

	public class NewsViewModelBase
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
	}

	public class CreateNewsViewModel : NewsViewModelBase
	{
		public List<IFormFile> Images { get; set; }
		[Required(ErrorMessage ="لطفا آیدی انجمن را وارد نمایید")]
		public int AssociationId { get; set; }
	}

	public class UpdateNewsViewModel :NewsViewModelBase
	{
		[Required(ErrorMessage ="لطفا آیدی خبر مورد نظر را وارد کنید")]
		public int Id { get; set; }
	}

	public class ChangeStatusNewsViewModel
	{
		[Required(ErrorMessage = "لطفا آیدی خبر مورد نظر را وارد کنید")]
		public int NewsId { get; set; }
		[Required(ErrorMessage = "لطفا آیدی وضعیت مورد نظر را وارد کنید")]
		public int StatusId { get; set; }
	}

	public class ChangeActiveNewsViewModel
	{
		[Required(ErrorMessage = "لطفا آیدی خبر مورد نظر را وارد کنید")]
		public int NewsId { get; set; }
	}

	public class AddImageNewsViewModel
	{
		public IFormFile Image { get; set; }
		[Required(ErrorMessage = "لطفا آیدی خبر مورد نظر را وارد کنید")]
		public int NewsId { get; set; }
	}

}
