using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneService.AutoMapperProfiles;
using MobilePhoneService.Controllers;
using MobilePhoneService.Repository;
using Moq;

namespace MobilePhoneServiceTests.Controllers
{
    public class ManufacturersControllerTest
    {
        [Fact]
        public void GetManufacturer_ReturnsNotFound()
        {

            var mockrepo = new Mock<IManufacturerRepository>();

            var mapperconfig = new MapperConfiguration(x => x.AddProfile(new MapProfiles()));
            var mapper = new Mapper(mapperconfig);
            var controller = new ManufacturersController(mockrepo.Object, mapper);

            var actioResult = controller.GetManufacturer(1) as NotFoundResult;
            Assert.NotNull(actioResult);
        }
    }
}
