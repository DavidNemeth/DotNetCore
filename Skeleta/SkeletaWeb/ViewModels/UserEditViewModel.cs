﻿using System.ComponentModel.DataAnnotations;

namespace SkeletaWeb.ViewModels
{
	public class UserEditViewModel : UserViewModel
	{
		public string CurrentPassword { get; set; }

		[MinLength(6, ErrorMessage = "New Password must be at least 6 characters")]
		public string NewPassword { get; set; }
		new private bool IsLockedOut { get; } //Hide base member
	}
}
