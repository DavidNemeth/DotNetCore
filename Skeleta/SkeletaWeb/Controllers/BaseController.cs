using Microsoft.AspNetCore.Mvc;
using SkeletaDAL;
using SkeletaWeb.Services;
using System;

namespace SkeletaWeb.Controllers
{
	public abstract class BaseController : Controller
	{
		protected IUnitOfWork context;
		protected IServices services;

		protected BaseController(IUnitOfWork context, IServices services)
		{
			this.context = context;
			this.services = services;
		}

		public String ErrorMessage
		{
			get { return TempData[nameof(ErrorMessage)] == null ? String.Empty : TempData[nameof(ErrorMessage)].ToString(); }
			set { TempData[nameof(ErrorMessage)] = value; }
		}

	}
}
