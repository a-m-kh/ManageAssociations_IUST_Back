using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	public class MovementFestivalViewModel
	{

	}

	public class CreateMovementFestivalViewModel
	{
	}


	public class CreateCompetitiveField
	{
		[Required(ErrorMessage = "لطفا آیدی جشنواره حرکت را پر نمایید.")]
		public int MovementFestivalId { get; set; }
	}

	public class CreatePositionViewModel
	{
		[Required(ErrorMessage = "لطفا آیدی عنوان را پر نمایید.")]
		public int TitleId { get; set; }
		public string Owner { get; set; }
		[Required(ErrorMessage ="لطفا آیدی حوزه رقابتی را پر نمایید.")]
		public int CompetiveFieldId { get; set; }
	}

	public class UpdatePositionViewModel
	{
		public int? TitleId { get; set; }
		public string Owner { get; set; }
		[Required(ErrorMessage = "لطفا آیدی مقام را پر نمایید.")]
		public int Id { get; set; }
	}


}
