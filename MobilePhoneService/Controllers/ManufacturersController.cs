using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneService.DTOs;
using MobilePhoneService.Repository;

namespace MobilePhoneService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IMapper _mapper;
        public ManufacturersController(IManufacturerRepository manufacturerRepository, IMapper mapper)
        {
            _manufacturerRepository = manufacturerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetProizvodjaci()
        {
            return Ok(_manufacturerRepository.GetAll().ProjectTo<ManufacturerDTO>(_mapper.ConfigurationProvider).ToList());
        }
        [HttpGet("{id}")]
        public IActionResult GetProizvodjac(int id)
        {
            var proizvodjac = _manufacturerRepository.GetById(id);
            if (proizvodjac == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ManufacturerDTO>(proizvodjac));
        }
        [HttpGet]
        [Route("/api/info")]
        public IActionResult GetProizvodjaciAndPhonesPrices(int granica)
        {
            return Ok(_manufacturerRepository.GetAllWithPhonesWithPrice(granica).ToList());
        }
        [HttpGet]
        [Route("/api/status")]
        public IActionResult GetProizvodjaciAndPhonesCount()
        {
            return Ok(_manufacturerRepository.GetAllWithPhonesCount().ToList());
        }
        [HttpGet]
        [Route("potrazi")]
        public IActionResult GetProizvodjaciByName(string ime)
        {
            return Ok(_manufacturerRepository.GetAllByName(ime).ProjectTo<ManufacturerDTO>(_mapper.ConfigurationProvider).ToList());
        }
    }
}
