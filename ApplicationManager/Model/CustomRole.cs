using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Extensitions.Model
{
	/// <summary>
	/// CustomRole
	/// </summary>
	public class CustomRole : IdentityRole<long>
	{
		/// <summary>
		/// CustomRole
		/// </summary>
		public CustomRole()
		{
		}

		/// <summary>
		/// CustomRole
		/// </summary>
		/// <param name="name"></param>
		public CustomRole(string name)
		{
			base.Name = name;
		}
	}
}
