﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses;

public class LoginResponse
{
	public string Token { get; set; }
	public string UserName { get; set; }
}
