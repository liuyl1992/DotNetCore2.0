using Common.Const;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core
{
	[Route("[controller]/[action]")]
	public abstract class BaseMvcController : Controller
	{
    }
}
