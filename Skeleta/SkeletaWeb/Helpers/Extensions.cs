﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SkeletaWeb.ViewModels;

namespace SkeletaWeb.Helpers
{
	public static class Extensions
	{
		public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
		{
			response.Headers.Add("Pagination", JsonConvert.SerializeObject(new PageHeader(currentPage, itemsPerPage, totalItems, totalPages)));
			response.Headers.Add("access-control-expose-headers", "Pagination");
		}

		public static void AddApplicationError(this HttpResponse response, string message)
		{
			response.Headers.Add("Application-Error", message);
			response.Headers.Add("access-control-expose-headers", "Application-Error");
		}
	}
}