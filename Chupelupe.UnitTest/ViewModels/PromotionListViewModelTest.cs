using System;
using AutoFixture;
using Chupelupe.Helpers;
using Chupelupe.Services;
using Chupelupe.ViewModels;
using Chupelupe.Models;
using System.Collections.Generic;
using Chupelupe.Views;
using Chupelupe.UnitTest.Helpers;
using Moq;
using NUnit.Framework;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace Chupelupe.UnitTest.ViewModels
{
    [TestFixture]
    public class PromotionListViewModelTest
    {
        Fixture Fixture { get; set; }
        DependencyServicesStub DependencyService { get; set; }
        Mock<IWebServiceApi> ServerSideDataMock { get; set; }
        Mock<INavigation> NavigationMock { get; set; }

        [SetUp]
        public void SetUp()
        {
            MockForms.Init();

            Fixture = new Fixture();
            DependencyService = new DependencyServicesStub();

            ServerSideDataMock = new Mock<IWebServiceApi>();
            DependencyService.Register<IWebServiceApi>(ServerSideDataMock.Object);

            NavigationMock = new Mock<INavigation>();
            DependencyService.Register<INavigation>(NavigationMock.Object);
        }

        [Test]
        public void GetPromotionsCommandIsSuccesful()
        {
            //Arrange
            var vm = new PromotionListViewModel(NavigationMock.Object, DependencyService);
            List<Promotion> list = new List<Promotion>
            {
                new Promotion
                {
                    Id = Fixture.Create<int>(),
                    Title = Fixture.Create<string>()
                }
            };
            ServerSideDataMock.Setup(m => m.GetPromotions()).ReturnsAsync(list);

            //Act
            vm.GetPromotionsCommand.Execute(null);

            //Assert
            ServerSideDataMock.Verify(m => m.GetPromotions(), Times.Once);
            Assert.IsNotNull(vm.ObjectList);
            Assert.AreEqual(1, vm.ObjectList.Count);
            Assert.IsFalse(vm.IsBusy);
        }
    }
}
