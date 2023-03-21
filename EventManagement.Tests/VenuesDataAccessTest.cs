using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManagement.Areas.Identity.Data;
using EventManagement.Controllers;
using EventManagement.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;



namespace EventManagement.Tests
{
    public class VenuesDataAccessTest
    {
        [Fact]
        public async Task VenuesListTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ApplicationDatabase")
            .Options;
            var context = new ApplicationDbContext(options);
            
            context.Venues.Add(new Venue
                {
                    VenueId = 1,
                    VenueName = "hall 1"
                });
            context.Venues.Add(new Venue
                {
                    VenueId = 2,
                    VenueName = "hall 2"
                });
            context.SaveChanges();
            List<Venue> venues = context.Venues.ToList();

            Assert.Equal(2,venues.Count);
        }
    }
}
