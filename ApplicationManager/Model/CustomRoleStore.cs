using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Identity.Extensitions.Model
{
	/// <summary>
	/// CustomRoleStore
	/// </summary>
	public class CustomRoleStore : RoleStore<CustomRole, ApplicationDbContext, long>
	{
		/// <summary>
		/// CustomRoleStore
		/// </summary>
		/// <param name="context"></param>
		public CustomRoleStore(ApplicationDbContext context) : base(context)
		{
		}
	}
}
