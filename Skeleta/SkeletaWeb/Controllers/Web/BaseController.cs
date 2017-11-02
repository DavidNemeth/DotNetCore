using Microsoft.AspNetCore.Mvc;
using SkeletaDAL;
using SkeletaWeb.Services;

namespace SkeletaWeb.Controllers.Web
{
	public class BaseController : Controller
	{
		protected IUnitOfWork context;
		protected IServices services;

		public BaseController(IUnitOfWork context, IServices services)
		{
			this.context = context;
			this.services = services;
		}
	}
}
