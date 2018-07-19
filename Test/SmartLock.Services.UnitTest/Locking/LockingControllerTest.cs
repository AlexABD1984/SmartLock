//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace SmartLock.Services.UnitTest.Locking
//{
//    public class LockingControllerTest
//    {
//        private readonly Mock<ILockServices> _catalogServiceMock;

//        public CatalogControllerTest()
//        {
//            _catalogServiceMock = new Mock<ICatalogService>();
//        }

//        [Fact]
//        public async Task OpenLock()
//        {
//            //Arrange
//            var fakeBrandFilterApplied = 1;
//            var fakeTypesFilterApplied = 2;
//            var fakePage = 2;
//            var fakeCatalog = GetFakeCatalog();

//            var expectedNumberOfPages = 5;
//            var expectedTotalPages = 50;
//            var expectedCurrentPage = 2;

//            _catalogServiceMock.Setup(x => x.GetCatalogItems
//            (
//                It.Is<int>(y => y == fakePage),
//                It.IsAny<int>(),
//                It.Is<int?>(y => y == fakeBrandFilterApplied),
//                It.Is<int?>(y => y == fakeTypesFilterApplied)
//             ))
//             .Returns(Task.FromResult(fakeCatalog));

//            //Act
//            var orderController = new CatalogController(_catalogServiceMock.Object);
//            var actionResult = await orderController.Index(fakeBrandFilterApplied, fakeTypesFilterApplied, fakePage, null);

//            //Assert
//            var viewResult = Assert.IsType<ViewResult>(actionResult);
//            var model = Assert.IsAssignableFrom<IndexViewModel>(viewResult.ViewData.Model);
//            Assert.Equal(model.PaginationInfo.TotalPages, expectedNumberOfPages);
//            Assert.Equal(model.PaginationInfo.TotalItems, expectedTotalPages);
//            Assert.Equal(model.PaginationInfo.ActualPage, expectedCurrentPage);
//            Assert.Empty(model.PaginationInfo.Next);
//            Assert.Empty(model.PaginationInfo.Previous);
//        }
//}
