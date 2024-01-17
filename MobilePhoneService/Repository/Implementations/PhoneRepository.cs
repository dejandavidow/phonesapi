using Microsoft.EntityFrameworkCore;
using MobilePhoneService.Data;
using MobilePhoneService.Models;
using System.Data;

namespace MobilePhoneService.Repository.Implementations
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly AppDbContext _appDbContext;
        public PhoneRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(Phone phone)
        {
            _appDbContext.Phones.Add(phone);
            _appDbContext.SaveChanges();
        }

        public void Delete(Phone phone)
        {
            _appDbContext.Phones.Remove(phone);
            _appDbContext.SaveChanges();
        }

        public IQueryable<Phone> GetAll()
        {
            return _appDbContext.Phones.Include(x => x.Manufacturer).OrderBy(x => x.Model);
        }

        public IQueryable<Phone> GetAllByManufacturerOrModel(string query)
        {
            return _appDbContext.Phones.Include(x => x.Manufacturer).Where(x => x.Model.Contains(query) || x.Manufacturer.Name.Contains(query)).OrderByDescending(x => x.Price);
        }

        public Phone GetById(int id)
        {
            return _appDbContext.Phones.Include(x => x.Manufacturer).SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<Phone> SearchPhones(int min, int max)
        {
            return _appDbContext.Phones.Include(x => x.Manufacturer).Where(x => x.Price >= min && x.Price <= max).OrderByDescending(x => x.Price);
        }

        public void Update(Phone phone)
        {
            try
            {
                _appDbContext.Phones.Update(phone);
                _appDbContext.SaveChanges();
            }
            catch (DBConcurrencyException)
            {
                throw;
            }
        }
    }
}
