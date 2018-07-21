using CacheManager.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SmartLock.Services.Locking.API.Controllers;
using SmartLock.Services.Locking.API.Model;
using SmartLock.Services.Locking.API.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartLock.Services.UnitTest.Locking
{
    public class LockingControllerTest
    {
        private readonly Mock<SmartLock.Services.Locking.API.ApplicationServices.LockingService> _lockingServiceMock;
        private readonly Mock<SmartLock.Services.Locking.API.ApplicationServices.AuditLogService> _auditLogServiceMock;
        private readonly Mock<SmartLock.Services.Locking.API.ApplicationServices.UserAccessService> _userAccessServiceMock;
        private readonly Mock<ILogger<LockingController>> _loggerMock;
        private readonly ILogger<LockingController> _logger;

        public LockingControllerTest()
        {
            _lockingServiceMock = new Mock<Services.Locking.API.ApplicationServices.LockingService>();
            _auditLogServiceMock = new Mock<Services.Locking.API.ApplicationServices.AuditLogService>();
            _userAccessServiceMock = new Mock<Services.Locking.API.ApplicationServices.UserAccessService>();
            _logger = Mock.Of<ILogger<LockingController>>();
        }

        [Fact]
        public async Task OpenLock()
        {
            //Arrange
            var fakeUserId = Guid.NewGuid();
            var fakeLockId = Guid.NewGuid();
            var fakeSecurityCode = "";
            OpenLockInfo openLockInfo = new OpenLockInfo(fakeUserId, fakeUserId, fakeSecurityCode);

            //_userAccessServiceMock.Setup(x => x.HasAccess
            //(
            //    It.IsAny<Guid>(),
            //    It.IsAny<Guid>()
            //))
            //.Returns(Task.FromResult<bool>(true));
            var lockingController = new Services.Locking.API.Controllers.LockingController(
                _logger, _userAccessServiceMock.Object, _lockingServiceMock.Object, _auditLogServiceMock.Object);

            //Act

            var actionResultSuccessful = await lockingController.Lock(openLockInfo);


            //Assert
            var okObjectResult = actionResultSuccessful as LockingResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.ResponseCode, -1);

            //var viewResult = Assert.IsType< OkObjectResult>(LockOpenSuccessful)>(actionResultSuccessful);

        }
    }
}
