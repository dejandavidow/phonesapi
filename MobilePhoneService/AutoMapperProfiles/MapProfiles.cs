using AutoMapper;
using MobilePhoneService.DTOs;
using MobilePhoneService.Models;

namespace MobilePhoneService.AutoMapperProfiles
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<Manufacturer, ManufacturerDTO>();
            CreateMap<Phone, PhoneDTO>();
        }
    }
}
