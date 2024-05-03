using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	internal class GuestLicenseViewModel
	{
	}

	public class GuestLicenseViewModelBase
	{
		
		public string? NationalCode { get; set; }
		public string? JobTitle { get; set; }

		public bool? IsGuest { get; set; }
		public bool? IsFromIust { get; set; }
		[Required(ErrorMessage = "لطفا آیدی انجمن را وارد نمایید")]
		public int AssociationId { get; set; }

	}

	public class CreateGuestLicenseViewModel : GuestLicenseViewModelBase
	{
		[Required(ErrorMessage = "لطفا نام مدعو را وارد نمایید")]
		public string Name { get; set; }

		[Required(ErrorMessage ="لطفا آیدی انجمن مد نظر را وارد نمایید")]
		public int AssociationId { get; set; }

	}

	public class UpdateGuestLicenseViewModel : GuestLicenseViewModelBase
	{
		[Required(ErrorMessage ="لطفا آیدی مجوز مدعو را وارد نمایید")]
		public int Id { get; set; }
		public string? Name { get; set; }
	}

	public class ChangeStatusGuestLicenseViewModel
	{
		[Required(ErrorMessage ="آیدی مجوز مدعو را وارد نمایید")]
		public int GuestLicenseId { get; set; }
		[Required(ErrorMessage = "آیدی وضعیت مدعو را وارد نمایید")]
		public int StatusId { get; set; }
	}
}
