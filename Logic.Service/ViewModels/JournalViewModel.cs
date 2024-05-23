using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	internal class JournalViewModel
	{
	}

	public class JournalViewModelBase
	{
		public virtual string? Name { get; set; }
		public virtual string? NoAndDate { get; set; }
		public virtual IFormFile? Pdf { get; set; }
		public virtual IFormFile? Image { get; set; }
	}
	public class CreateJournalViewModel : JournalViewModelBase
	{
		[Required(ErrorMessage = "لطفا نام نشریه را وارد نمایید")]
		public override string Name { get; set; }
		[Required(ErrorMessage = "لطفا آیدی انجمن را وارد نمایید")]
		public int AssociationId { get; set; }
	}

	public class UpdateJournalViewModel : JournalViewModelBase
	{
		[Required(ErrorMessage = "لطفا آیدی نشریه را وارد نمایید")]
		public int Id { get; set; }
	}
}
