using Common.Const;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
namespace Common.Core
{
	[Route("api/[controller]")]
	[EnableCors(ConstValues.CorsValue)] //设置跨域处理（startup文件设置全局跨域，有无此代码不影响跨域）
	public abstract class BaseChaunceController : Controller
	{
		protected IActionResult SendResult(ErrorCode errorCode = ErrorCode.OK, object result = null, params string[] message)
		{
			return Json(new { errorCode, result, message });
		}
	}
}
