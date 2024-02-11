using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels;

public class LoginViewModel
{
	[Required(ErrorMessage ="نام کاربری نباید خالی باشد")]
	public string UserName { get; set; }
	[Required(ErrorMessage ="رمز را وارد نمایید")]
	public string Password { get; set; }
}
