using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common
{
	public enum ErrorCode
	{
		/// <summary>
		/// 一切正常
		/// </summary>
		[Description("一切正常")]
		OK = 200,

		/// <summary>
		/// 錯誤
		/// </summary>
		[Description("错误")]
		Exception = 500,
	}
}
