using MobilePhoneService.Models;

namespace MobilePhoneService.Repository
{
    public interface IPhoneRepository
    {
        IQueryable<Phone> GetAll();
        Phone GetById(int id);
        IQueryable<Phone> GetAllByManufacturerOrModel(string query);
        void Create(Phone phone);
        void Update(Phone phone);
        void Delete(Phone phone);
        IQueryable<Phone> SearchPhones(int min, int max);
    }
}
