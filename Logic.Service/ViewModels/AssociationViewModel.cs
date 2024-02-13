using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels;

internal class AssociationViewModel
{
}

public class AssociationViewModelBase
{
	[Required(ErrorMessage ="لطفا نام انجمن را وارد نمایید.")]
	public virtual string Name { get; set; }
	public IFormFile? Logo { get; set; }
}

public class CreateAssociationViewModel : AssociationViewModelBase
{

}

public class UpdateAssociationViewModel : AssociationViewModelBase
{

	public int Id { get; set; }
	public override string? Name { get; set; }
}


