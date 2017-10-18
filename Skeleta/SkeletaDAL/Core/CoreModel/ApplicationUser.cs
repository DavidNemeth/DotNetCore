﻿using Microsoft.AspNetCore.Identity;
using SkeletaDAL.Model;
using SkeletaDAL.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace SkeletaDAL.Core.CoreModel
{
	public class ApplicationUser : IdentityUser, IAuditableEntity
	{
		public virtual string FriendlyName
		{
			get
			{
				var friendlyName = string.IsNullOrWhiteSpace(FullName) ? UserName : FullName;

				if (!string.IsNullOrWhiteSpace(JobTitle))
					friendlyName = JobTitle + " " + friendlyName;

				return friendlyName;
			}
		}


		public string JobTitle { get; set; }
		public string FullName { get; set; }
		public string Configuration { get; set; }
		public bool IsEnabled { get; set; }
		public bool IsLockedOut => this.LockoutEnabled && this.LockoutEnd >= DateTimeOffset.UtcNow;

		public string CreatedBy { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }



		/// <summary>
		/// Navigation property for the roles this user belongs to.
		/// </summary>
		public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

		/// <summary>
		/// Navigation property for the claims this user possesses.
		/// </summary>
		public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

		/// <summary>
		/// Demo Navigation property for orders this user has processed
		/// </summary>
		public ICollection<Order> Orders { get; set; }
	}
}