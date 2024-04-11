using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	internal class GuestViewModel
	{
	}

	public class GuestViewModelBase
	{
		public virtual string? Name { get; set; }
		public virtual string? Title { get; set; }
	}

	public class CreateGuestViewModel : GuestViewModelBase
	{
		[Required(ErrorMessage = "لطفا آیدی رویداد را وارد نمایید")]
		public int EventId { get; set; }
		public IFormFile? Image { get; set; }

		[Required(ErrorMessage = "لطفا نام سخنران را وارد نمایید")]
		public override string Name { get; set; }
		[Required(ErrorMessage = "لطفا عنوان سخنران را وارد نمایید")]
		public override string Title { get; set; }
	}

	public class UpdateGuestViewModel : GuestViewModelBase
	{
		[Required(ErrorMessage = "لطفا آیدی سخنران را وارد نمایید")]
		public int Id { get; set; }

		/*[Required(ErrorMessage = "لطفا آیدی انجمن را وارد نمایید")]
		public int AssociationId { get; set; }*/
		public IFormFile? Image { get; set; }

		
	}
}
