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
        private readonly Mock<SmartLock.Services.Locking.API.ApplicationServices.ILockingService> _lockingServiceMock;
        private readonly Mock<SmartLock.Services.Locking.API.ApplicationServices.IAuditLogService> _auditLogServiceMock;
        private readonly Mock<SmartLock.Services.Locking.API.ApplicationServices.IUserAccessService> _userAccessServiceMock;
        private readonly ILogger<LockingController> _logger;

        public LockingControllerTest()
        {
            _lockingServiceMock = new Mock<Services.Locking.API.ApplicationServices.ILockingService>();
            _auditLogServiceMock = new Mock<Services.Locking.API.ApplicationServices.IAuditLogService>();
            _userAccessServiceMock = new Mock<Services.Locking.API.ApplicationServices.IUserAccessService>();
            _logger = Mock.Of<ILogger<LockingController>>();
        }

        [Fact(DisplayName = "OpenLocSuccessful")]
        public async Task OpenLockTest_HasAccess_And_Successful()
        {
            //Arrange
            var fakeUserId = Guid.NewGuid();
            var fakeLockId = Guid.NewGuid();
            var fakeSecurityCode = "";
            OpenLockInfo openLockInfo = new OpenLockInfo(fakeUserId, fakeUserId, fakeSecurityCode);

            _userAccessServiceMock.Setup(x => x.HasAccess
            (
                It.IsAny<Guid>(),
                It.IsAny<Guid>()
            ))
            .Returns(Task.FromResult<bool>(true));
            var lockingController = new Services.Locking.API.Controllers.LockingController(
                _logger, _userAccessServiceMock.Object, _lockingServiceMock.Object, _auditLogServiceMock.Object);

            //Act
            var actionResultSuccessful = await lockingController.Lock(openLockInfo);


            //Assert
            var okObjectResult = actionResultSuccessful as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(1, (okObjectResult.Value as LockOpenSuccessful).ResponseCode);           
        }
    }
}
