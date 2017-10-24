using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Identity.Extensitions.Model
{
	/// <summary>
	/// CustomUserStore
	/// </summary>
	public class CustomUserStore : UserStore<ApplicationUser, CustomRole, ApplicationDbContext, long>
	{
		/// <summary>
		/// CustomUserStore
		/// </summary>
		/// <param name="context"></param>
		public CustomUserStore(ApplicationDbContext context) : base(context)
		{
		}
	}
}
