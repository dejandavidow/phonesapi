using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneService.DTOs;
using MobilePhoneService.Models;
using MobilePhoneService.Repository;
using System.Data;

namespace MobilePhoneService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMapper _mapper;
        public PhonesController(IPhoneRepository phoneRepository, IMapper mapper)
        {
            _mapper = mapper;
            _phoneRepository = phoneRepository;
        }
        [HttpGet]
        public IActionResult GetPhones()
        {
            return Ok(_phoneRepository.GetAll().ProjectTo<PhoneDTO>(_mapper.ConfigurationProvider).ToList());
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetPhone(int id)
        {
            var phone = _phoneRepository.GetById(id);
            if (phone == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PhoneDTO>(phone));
        }
        [Authorize]
        [HttpGet]
        [Route("trazi")]
        public IActionResult GetPhonesByManufacturerOrModel(string query)
        {
            return Ok(_phoneRepository.GetAllByManufacturerOrModel(query).ProjectTo<PhoneDTO>(_mapper.ConfigurationProvider).ToList());
        }
        [Authorize]
        [HttpPost]
        public IActionResult PostPhone(Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _phoneRepository.Create(phone);
            return CreatedAtAction(nameof(GetPhone), new { id = phone.Id }, phone);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutPhone(int id, Phone phone)
        {
            if (id != phone.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _phoneRepository.Update(phone);
            }
            catch (DBConcurrencyException)
            {
                return BadRequest();
            }
            return Ok(phone);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeletePhone(int id)
        {
            var phone = _phoneRepository.GetById(id);
            if (phone == null)
            {
                return NotFound();
            }
            _phoneRepository.Delete(phone);
            return NoContent();
        }
        [Authorize]
        [HttpPost]
        [Route("/api/pretraga")]
        public IActionResult SearchPhones(SearchParameters search)
        {
            if (search.Min > search.Max || search.Min <= 0 || search.Max <= 0)
            {
                return BadRequest();
            }
            return Ok(_phoneRepository.SearchPhones(search.Min, search.Max).ProjectTo<PhoneDTO>(_mapper.ConfigurationProvider).ToList());
        }
    }
}
