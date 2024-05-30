using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class FormDto
	{
	}

	public class FormUpdateDto
	{
		[Required(ErrorMessage ="لطفا آیدی فرم مد نظر را وارد نمایید")]
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Url { get; set; }
	}

	public class FormGetDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Url { get; set; }
	}

}
