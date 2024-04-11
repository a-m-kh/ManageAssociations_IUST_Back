using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	internal class AssociationMemberViewModel
	{
	}

	public class AssociationMemberViewModelBase
	{
		public virtual string? Name { get; set; }
		public virtual string? Role { get; set; }
		public virtual IFormFile? Image { get; set; }
	}

	public class CreateAssociationMemberViewModel : AssociationMemberViewModelBase
	{
		[Required(ErrorMessage = "لطفا عضو انجمن را وارد نمایید")]
		public override string Name { get; set; }
		[Required(ErrorMessage = "لطفا آیدی انجمن را وارد نمایید")]
		public int AssociationId { get; set; }
	}

	public class UpdateAssociationMemberViewModel : AssociationMemberViewModelBase
	{
		[Required(ErrorMessage = "لطفا آیدی عضو انجمن را وارد نمایید")]
		public int Id { get; set; }
	}
}
