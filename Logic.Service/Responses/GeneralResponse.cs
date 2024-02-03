using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses;

public class GeneralResponse<T>
{
	public T Data { get; set; }
	public string Message { get; set; }
	public int Status { get; set; }
	public bool IsSuccess { get; set; }

	public GeneralResponse(T Data)
	{
		this.Data = Data;
	}
	public GeneralResponse()
	{

	} 
}
