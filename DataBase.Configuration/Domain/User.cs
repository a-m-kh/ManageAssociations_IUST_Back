using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain;

public class User : IdentityUser<string>
{
	public Association? Association { get; set; }
}
