using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneService.AutoMapperProfiles;
using MobilePhoneService.Controllers;
using MobilePhoneService.DTOs;
using MobilePhoneService.Models;
using MobilePhoneService.Repository;
using Moq;

namespace MobilePhoneServiceTests.Controllers
{
    public class PhonesControllerTest
    {
        [Fact]
        public void GetPhone_ValidId_ReturnsOkAndObject()
        {
            var telefon = new Phone() { Id = 1, Model = "S20", OperatingSystem = "Android", Size=5, Price=500, ManufacturerId=1, Manufacturer = new Manufacturer() { Id =1, Name="Samsung", Country="Koreja" } };
            var dto = new PhoneDTO() { Id = 1, Model = "S20", OperatingSystem = "Android", Size=5, Price=500, ManufacturerName = "Samsung" };

            var mockrepo = new Mock<IPhoneRepository>();
            mockrepo.Setup(x => x.GetById(1)).Returns(telefon);

            var mapperconfig = new MapperConfiguration(x => x.AddProfile(new MapProfiles()));
            var mapper = new Mapper(mapperconfig);
            var controller = new PhonesController(mockrepo.Object, mapper);

            var actioResult = controller.GetPhone(1) as OkObjectResult;
            Assert.NotNull(actioResult);
            Assert.NotNull(actioResult.Value);

            Assert.Equal(telefon.Id, dto.Id);
            Assert.Equal(telefon.Model, dto.Model);
            Assert.Equal(telefon.OperatingSystem, dto.OperatingSystem);
            Assert.Equal(telefon.Size, dto.Size);
            Assert.Equal(telefon.Price, dto.Price);
            Assert.Equal(telefon.Manufacturer.Name, dto.ManufacturerName);
        }
        [Fact]
        public void PutPhone_invalidId_ReturnsBadRequst()
        {
            var telefon = new Phone() { Id = 1, Model = "S20", OperatingSystem = "Android", Size=5, Price=500, ManufacturerId=1, Manufacturer = new Manufacturer() { Id =1, Name="Samsung", Country="Koreja" } };

            var mockrepo = new Mock<IPhoneRepository>();

            var mapperconfig = new MapperConfiguration(x => x.AddProfile(new MapProfiles()));
            var mapper = new Mapper(mapperconfig);
            var controller = new PhonesController(mockrepo.Object, mapper);

            var actioResult = controller.PutPhone(5, telefon) as BadRequestResult;
            Assert.NotNull(actioResult);
        }
        [Fact]
        public void SearchPhones_ReturnsCollection()
        {
            List<Phone> telefoni = new List<Phone>()
            {
                new Phone() { Id = 1, Model="A94", OperatingSystem = "Android", Size=12, Price = 31125.42m, ManufacturerId = 3,Manufacturer = new Manufacturer(){ Id = 3,Name="Tesla",Country = "Kina"} },
                new Phone() { Id = 2, Model="13T Pro", OperatingSystem = "Android", Size=7, Price = 104999.99m, ManufacturerId = 1,Manufacturer = new Manufacturer(){ Id = 1,Name="Huawei",Country = "Kina"} },
            };

            var searchDTO = new SearchParameters() { Min=100, Max=1000 };

            var mockrepo = new Mock<IPhoneRepository>();
            mockrepo.Setup(x => x.SearchPhones(100, 1000)).Returns(telefoni.AsQueryable());

            var mapperconfig = new MapperConfiguration(x => x.AddProfile(new MapProfiles()));
            var mapper = new Mapper(mapperconfig);
            var controller = new PhonesController(mockrepo.Object, mapper);

            var actionResult = controller.SearchPhones(searchDTO) as OkObjectResult;
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            var dtos = (List<PhoneDTO>)actionResult.Value;
            for (int i = 0; i < dtos.Count; i++)
            {
                Assert.Equal(telefoni[i].Id, dtos[i].Id);
                Assert.Equal(telefoni[i].Model, dtos[i].Model);
                Assert.Equal(telefoni[i].OperatingSystem, dtos[i].OperatingSystem);
                Assert.Equal(telefoni[i].Size, dtos[i].Size);
                Assert.Equal(telefoni[i].Price, dtos[i].Price);
                Assert.Equal(telefoni[i].Manufacturer.Name, dtos[i].ManufacturerName);
            }
        }
    }
}
