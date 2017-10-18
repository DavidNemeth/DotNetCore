using Blank.DAL.Interfaces;
using Blank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blank.DAL
{
		public class BlankRepository : IBlankRepository
		{
				private ILogger<BlankRepository> logger;
				private BlankContext context;

				public BlankRepository(BlankContext context, ILogger<BlankRepository> logger)
				{
						this.logger = logger;
						this.context = context;
				}

				public void AddStop(string tripName, string username, Stop newStop)
				{
						var trip = GetTripByName(tripName, username);

						if (trip != null)
						{
								trip.Stops.Add(newStop);
						}
				}

				public void AddTrip(Trip trip)
				{
						context.Add(trip);
				}

				public IEnumerable<Trip> GetAllTrips()
				{
						logger.LogInformation("Getting All Trips from the Database");

						return context.Trips.ToList();
				}

				public Trip GetTripByName(string tripName, string username)
				{
						return context.Trips
							.Include(t => t.Stops)
							.Where(t => t.Name == tripName && t.UserName == username)
							.FirstOrDefault();
				}

				public List<Trip> GetTripsByWhatever<T>(T searchParameter)
				{
						List<Trip> filteredTrips = new List<Trip>();
						var type = searchParameter.GetType();

						if (type == typeof(string))
						{
								filteredTrips = context.Trips
										.Where(t => t.UserName.Equals(searchParameter)).ToList();
						}

						if (type == typeof(int))
						{
								filteredTrips = context.Trips
										.Where(t => t.Id.Equals(searchParameter)).ToList();
						}

						if (type == typeof(DateTime))
						{
								filteredTrips = context.Trips
										.Where(t => t.DateCreated.Equals(searchParameter)).ToList();
						}

						return filteredTrips;
				}

				public async Task<bool> SaveChangesAsync()
				{
						return (await context.SaveChangesAsync()) > 0;
				}
		}
}
