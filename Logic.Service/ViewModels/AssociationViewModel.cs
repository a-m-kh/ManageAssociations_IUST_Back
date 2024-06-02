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
	public string? Phone { get; set; }
	public string? Email { get; set; }
	public string? Address { get; set; }
}

public class CreateAssociationViewModel : AssociationViewModelBase
{
	[Required(ErrorMessage = "لطفا نام کاربری را وارد نمایید.")]
	public string UserName { get; set; }
	[Required(ErrorMessage = "لطفا رمز را وارد نمایید.")]
	public string Password { get; set; }
}

public class UpdateAssociationViewModel
{
	public string? Name { get; set; }
	public IFormFile? Logo { get; set; }
	public string? Phone { get; set; }
	public string? Email { get; set; }
	public string? Address { get; set; }
	[Required(ErrorMessage = "لطفا آیدی انجمن را وارد نمایید.")]
	public int Id { get; set; }
}


