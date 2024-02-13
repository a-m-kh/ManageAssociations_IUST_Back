using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	internal class EventViewModel
	{
	}

	public class EventBaseViewModel
	{

		public string? Title { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public string? Description { get; set; }
		public int? Price { get; set; }
		public IFormFile? Image { get; set; }
		public int AssociationId { get; set; }
	}

	public class EventCreateViewModel : EventBaseViewModel
	{
		public int? PeriodID { get; set; }
		public int? TypeOfEventID { get; set; }
		public int? IssueID { get; set; }
	}

	public class EventUpdateViewModel: EventCreateViewModel
	{
		[Required(ErrorMessage ="آیدی رویداد موردنظر را وارد کنید")]
		public int Id { get; set; }
	}

	public class EventDeletetViewModel
	{
		[Required(ErrorMessage = "آیدی رویداد موردنظر را وارد کنید")]
		public int Id { get; set; }
	}

	public class EventGetViewModel
	{
		[Required(ErrorMessage = "آیدی رویداد موردنظر را وارد کنید")]
		public int Id { get; set; }
	}
}
