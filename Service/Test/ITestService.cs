
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
	public interface ITestService
	{
		string Test();
	}

	internal interface ITestinternalService : ITestService
	{

	}
}
