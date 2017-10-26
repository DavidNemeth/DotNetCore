using FluentValidation;
using System.Collections.Generic;

namespace SkeletaWeb.ViewModels
{
	public class CustomerViewModel : ViewModelBase
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Gender { get; set; }

		public IEnumerable<OrderViewModel> Orders { get; set; }
	}

	public class CustomerViewModelValidator : AbstractValidator<CustomerViewModel>
	{
		public CustomerViewModelValidator()
		{
			RuleFor(register => register.FirstName).NotEmpty().WithMessage("Customer name cannot be empty");
			RuleFor(register => register.Gender).NotEmpty().WithMessage("Gender cannot be empty");
		}
	}
}