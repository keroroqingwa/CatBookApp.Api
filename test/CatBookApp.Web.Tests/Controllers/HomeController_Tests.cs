using System.Threading.Tasks;
using CatBookApp.Web.Controllers;
using Shouldly;
using Xunit;

namespace CatBookApp.Web.Tests.Controllers
{
    public class HomeController_Tests: CatBookAppWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
