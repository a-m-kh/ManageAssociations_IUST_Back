﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Customize;

public class OptionalEmailUserValidator<TUser> : UserValidator<TUser> where TUser : class
{
	public OptionalEmailUserValidator(IdentityErrorDescriber errors = null) : base(errors)
	{
	}

	public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
	{
		try
		{
			var result = await base.ValidateAsync(manager, user);
			if (!result.Succeeded && string.IsNullOrWhiteSpace(await manager.GetEmailAsync(user)))
			{
				var errors = result.Errors.Where(e => e.Code != "InvalidEmail");
				result = errors.Count() > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
			}
			return result;
		}catch(Exception ex)
		{
			throw new Exception();
		}
		
	}
}
