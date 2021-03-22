using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using MeterApp.ViewModels;
using AutoMapper;
using System.Net;
using MeterApp.Utils;
using DAL.Core.Interfaces;

namespace MeterApp.Controllers
{
    [ApiController]
    [Route("api/meter")]
    public class MeterController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ILogger<MeterController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMeterService _meterService;

        public MeterController(ILogger<MeterController> logger, IUnitOfWork unitOfWork, IMapper mapper, IMeterService meterService)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _meterService = meterService;
        }

        [HttpPost]
        public IActionResult AddHouse(HouseVM house)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest(new { success = false, errors = ModelState.Errors() });
            }

            try
            {
                var newHouse = _mapper.Map<HouseVM, House>(house);
                _unitOfWork.Houses.Add(newHouse);
                _unitOfWork.SaveChanges();
                return Ok(new { success = true, message = "Доб успешно добавлен." });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Произошла ошибка, обратитесь за помощью к администратору. {ex.Message}");
                return BadRequest(new { success = false, errors = ModelState.Errors() });
            }
        }

        [HttpDelete]
        public IActionResult RemoveHouse(int houseId)
        {
            if (houseId <= 0)
            {
                BadRequest(new { success = false, message = "Некорректный идентификатор дома." });
            }

            var house = _unitOfWork.Houses.Get(houseId);

            if (house != null)
            {
                _unitOfWork.Houses.Remove(house);
                _unitOfWork.SaveChanges();
                return Ok(new { success = true, message = "Дом успешно удален из реестра." });
            }

            return NotFound(new { success = false, message = "Дом не найден" });

        }

        [HttpGet]
        [Route("getAllHousesById")]
        public IActionResult GetHouseById(int houseId)
        {
            if (houseId <= 0)
            {
                BadRequest(new { success = false, message = "Некорректный идентификатор дома." });
            }

            var house = _unitOfWork.Houses.Get(houseId);

            if (house != null)
            {
                return Ok(_mapper.Map<House, HouseVM>(house));
            }

            return NotFound(new { success = false, message = "Дом не найден" });
        }

        [HttpGet]
        [Route("getallhouses")]
        public async  Task<IActionResult> GetAllHouses()
        {
            var houses = await _unitOfWork.Houses.GetAllAsync();

            if (houses.Any())
                return Ok(houses);

            return NoContent();
        }

        [HttpPut]
        public IActionResult AddMeterToHouse(int houseId, string meterGuid)
        {
            if (houseId <= 0)
            {
                BadRequest(new { success = false, message = "Некорректный идентификатор дома." });
            }

            if (string.IsNullOrEmpty(meterGuid))
            {
                BadRequest(new { success = false, message = "Ввдите идентификатор счетчика." });
            }

            try
            {
                var house = _unitOfWork.Houses.Get(houseId);
                if (house != null)
                {
                    house.MeterGiud = meterGuid;
                    _unitOfWork.Houses.Update(house);
                    _unitOfWork.SaveChanges();

                    return Ok(new { success = true, message = "Счетчик успешно добавлен." });
                }

                return NotFound(new { success = false, message = "Дом не найден" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Произошла ошибка, обратитесь за помощью к администратору. {ex.Message}");
                return BadRequest(new { success = false, errors = ModelState.Errors() });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetHouseWithMaxMeterQnty()
        {
            var maxHouse = await _meterService.HouseWithMaxOrMinMeterQnty(true);

            if (maxHouse.Any())
                return Ok(maxHouse); 

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetHouseWithMinMeterQnty()
        {
            var maxHouse = await _meterService.HouseWithMaxOrMinMeterQnty(false);

            if (maxHouse.Any())
                return Ok(maxHouse);

            return NoContent();
        }

        [HttpPost]
        public IActionResult AddMeteringByMeterGiud(MeterVM newMeter)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest(new { success = false, errors = ModelState.Errors() });
            }

            if (string.IsNullOrEmpty(newMeter?.MeterGuid))
            {
                BadRequest(new { success = false, message = "Ввдите идентификатор счетчика." });
            }

            try
            {
                var house = _unitOfWork.Houses.Find(x => x.MeterGiud.ToLower() == newMeter.MeterGuid.ToLower()).FirstOrDefault();

                if (house == null)
                    return NotFound(new { success = false, message = "Указанный счетчик не зарегистрирован." });

                var newMetering = new Meter
                {
                    HouseId = house.Id,
                    DateMeter = DateTime.Now,
                    Quantity = newMeter.Quantity
                };

                _unitOfWork.Meters.Add(newMetering);
                _unitOfWork.SaveChanges();

                return Ok(new { success = true, message = "Показания успешно добавлены." });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Произошла ошибка, обратитесь за помощью к администратору. {ex.Message}");
                return BadRequest(new { success = false, errors = ModelState.Errors() });
            }
        }

        [HttpPost]
        public IActionResult AddMeteringByHouseId(MeterVM newMeter)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest(new { success = false, errors = ModelState.Errors() });
            }

            if (newMeter?.HouseId <= 0)
            {
                BadRequest(new { success = false, message = "Укажите дом." });
            }

            try
            {
                var house = _unitOfWork.Houses.Get(newMeter.HouseId); 

                if (house == null)
                    return NotFound(new { success = false, message = "Дом не найден." });

                var newMetering = new Meter
                {
                    HouseId = house.Id,
                    DateMeter = DateTime.Now,
                    Quantity = newMeter.Quantity
                };

                _unitOfWork.Meters.Add(newMetering);
                _unitOfWork.SaveChanges();

                return Ok(new { success = true, message = "Показания успешно добавлены." });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Произошла ошибка, обратитесь за помощью к администратору. {ex.Message}");
                return BadRequest(new { success = false, errors = ModelState.Errors() });
            }
        }
    }
}
