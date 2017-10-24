using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Identity.Extensitions.Extensitions
{
	/// <summary>
	/// IdentityExtensions
	/// </summary>
	public static class IdentityExtensions
	{
		/// <summary>
		/// 从cookie取出对应值
		/// </summary>
		/// <param name="identity"></param>
		/// <returns></returns>
		public static string GetValueFromCookie(this IIdentity identity, HttpContext context, string key)
		{
			string value = context.Request.Cookies[key];

			return Convert.ToString(value ?? string.Empty);
		}

		/// <summary>
		/// 批量设定cookie
		/// </summary>
		/// <param name="identity"></param>
		/// <param name="nickName"></param>
		/// <returns></returns>
		public static void SetValueToCookie(this IIdentity identity, HttpContext context, Dictionary<string, string> keyValue)
		{
			foreach (var item in keyValue)
			{
				context.Response.Cookies.Append(item.Key, item.Value);
			}
		}

		/// <summary>
		/// 取回當前使用者暱稱
		/// </summary>
		/// <param name="identity"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetClaimValue(this IIdentity identity, string key)
		{
			Claim claim = ((ClaimsIdentity)identity).FindFirst(key);
			if (claim == null)
			{
				return string.Empty;
			}
			return claim.Value;
		}
	}
}
