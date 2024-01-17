using MobilePhoneService.DTOs;
using MobilePhoneService.Models;

namespace MobilePhoneService.Repository
{
    public interface IManufacturerRepository
    {
        IQueryable<Manufacturer> GetAll();
        Manufacturer GetById(int id);
        IEnumerable<ManufacturerPhoneMinimalAveragePrice> GetAllWithPhonesWithPrice(int limit);
        IEnumerable<ManufacuterPhonesModelsCount> GetAllWithPhonesCount();
        IQueryable<Manufacturer> GetAllByName(string name);
    }
}
