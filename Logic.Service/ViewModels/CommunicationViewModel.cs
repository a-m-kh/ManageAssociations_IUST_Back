using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	internal class CommunicationViewModel
	{
	}

	public class CommunicationViewModelBase
	{

		public virtual string? Title { get; set; }
		public virtual string? Link { get; set; }
	}

	public class CreateCommunicationViewModel : CommunicationViewModelBase
	{
		[Required(ErrorMessage ="لطفا انجمن مدنظر را انتخاب کنید")]
		public int AssociationId { get; set; }

		[Required(ErrorMessage = "لطفا نوع لینک مدنظر را انتخاب کنید")]
		public int TypeOfLinkId { get; set; }

		[Required(ErrorMessage = "لطفا عنوان مدنظر را انتخاب کنید")]
		public override string Title { get; set; }

		[Required(ErrorMessage = "لطفا لینک مدنظر را کامل کنید")]
		public override string Link { get; set; }
	}

	public class UpdateCommunicationViewModel : CommunicationViewModelBase
	{
		[Required(ErrorMessage = "لطفا انجمن مدنظر را انتخاب کنید")]
		public int AssociationId { get; set; }
		[Required(ErrorMessage = "لطفا آیدی مدنظر را وارد کنید")]
		public int Id { get; set; }
		public int? TypeOfLinkId { get; set; }

	}
}
