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

namespace MeterApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MeterController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public MeterController(ILogger<MeterController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
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

    }
}
