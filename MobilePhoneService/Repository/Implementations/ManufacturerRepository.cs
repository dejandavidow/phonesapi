using Microsoft.EntityFrameworkCore;
using MobilePhoneService.Data;
using MobilePhoneService.DTOs;
using MobilePhoneService.Models;

namespace MobilePhoneService.Repository.Implementations
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly AppDbContext _appDbContext;
        public ManufacturerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IQueryable<Manufacturer> GetAll()
        {
            return _appDbContext.Manufacturers.OrderBy(x => x.Name);
        }

        public IQueryable<Manufacturer> GetAllByName(string name)
        {
            return _appDbContext.Manufacturers.Where(x => x.Name.Equals(name)).OrderBy(x => x.Country).ThenByDescending(x => x.Name);
        }

        public IEnumerable<ManufacuterPhonesModelsCount> GetAllWithPhonesCount()
        {
            return _appDbContext.Manufacturers.Include(x => x.ManufacturerPhones).Select(x => new ManufacuterPhonesModelsCount()
            {
                Name = x.Name,
                ModelCount = x.ManufacturerPhones.Select(x => x.Model).Count(),
                SizeCount = x.ManufacturerPhones.Select(x => x.Size).Count()
            }).OrderByDescending(x => x.ModelCount);
        }

        public IEnumerable<ManufacturerPhoneMinimalAveragePrice> GetAllWithPhonesWithPrice(int limit)
        {
            return _appDbContext.Manufacturers.Include(x => x.ManufacturerPhones).Select(x => new ManufacturerPhoneMinimalAveragePrice()
            {
                Name = x.Name,
                MinPhonePrice = x.ManufacturerPhones.Select(c => c.Price).Min(),
                AveragePhonePrice = x.ManufacturerPhones.Select(c => c.Price).Average()
            }).Where(x => x.AveragePhonePrice < limit).OrderBy(x => x.Name);
        }

        public Manufacturer GetById(int id)
        {
            return _appDbContext.Manufacturers.SingleOrDefault(x => x.Id == id);
        }
    }
}
