using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SmartLock.Services.AuditLogs.API.Controllers;
using SmartLock.Services.AuditLogs.API.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartLock.Services.AuditLogs.API.Controllers
{
    public class  AuditLogControllerTests
    {
        //[Fact(DisplayName = "Index_ReturnsAViewResult_WithAListOfAuditLog")]
        //public async Task Index_ReturnsAViewResult_WithAListOfAuditLog()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IBrainstormSessionRepository>();
        //    mockRepo.Setup(repo => repo.ListAsync()).Returns(Task.FromResult(GetTestSessions()));
        //    var controller = new AuditLogsController(mockRepo.Object);
        //    var controller = new PersonsController(new PersonService());

        //    // Act
        //    var result = await controller.GetAuditLogs();
        //    // Assert
        //    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        //    var persons = okResult.Value.Should().BeAssignableTo<IEnumerable<Person>>().Subject;

        //    persons.Count().Should().Be(50);
        //}

        [Fact(DisplayName = "Index_Returns_ViewResult_With_ListOfAuditLog")]
        public async Task TestGetAuditLogs()
        {
            using (var context = GetContextWithData())
            {
                // Arrange
                var controller = new AuditLogsController(context);

                //Act
                var result = controller.GetAuditLogs(1,10);

                //Assert
                Assert.NotNull(result);
                //Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
            }
        }

        private AuditLogDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<AuditLogDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            var context = new AuditLogDbContext(options);
            var auditLog1 = new SmartLock.Services.AuditLogs.API.Model.AuditLog { AuditLogId = Guid.NewGuid(),
                 UserId=Guid.NewGuid(), LockId=Guid.NewGuid(), RequestDate=DateTime.Now, Command="Open" };

            context.AuditLogs.Add(auditLog1);

            context.SaveChanges();

            return context;
        }
    }
}
