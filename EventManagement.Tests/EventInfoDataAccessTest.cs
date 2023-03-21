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
    public class EventInfoDataAccessTest
    {
        [Fact]
        public async Task EventListTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ApplicationDatabase")
            .Options;
            var context = new ApplicationDbContext(options);

            context.EventInformations.Add(new EventInformation
            {
                EventInfoId = 1,
                EventTime = DateTime.Now,
                AttendeeNumber = 12,
                UserId = "qwer3456",
                EventCategoryId = 1,
                VenueId = 1,
                BookedOn = DateTime.Now,
            });
            context.SaveChanges();
            List<EventInformation>eventinfos = context.EventInformations.ToList();

            Assert.Equal(1, eventinfos.Count);
        }
    }
}
