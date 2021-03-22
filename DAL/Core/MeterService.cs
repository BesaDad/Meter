// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class MeterService : IMeterService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MeterService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Т.к. у нескольких домов могут быть одинаковые максимальные суммарные показания, метода выводит их всех.
        /// </summary>
        /// <param name="isMax">Если 'true', то ищем максимальные показания, если false - 'минимальные'.</param>
        public async Task<IEnumerable<House>> HouseWithMaxOrMinMeterQnty(bool isMax) 
        {
            var houses = await _unitOfWork.Houses.GetAllAsync();
            var meters = await _unitOfWork.Meters.GetAllAsync();

            var sumQntyOfHouses = houses
                .Join(meters, house => house.Id, t => t.HouseId, (house, t) => new { house, t })
                .GroupBy(x => new { x.house.Id })
                .Select(g => new
                {
                    g.Key.Id,
                    SumOfMeterQnty = g.Sum(x => x.t.Quantity),
                });

            var sumQnty= isMax ? sumQntyOfHouses.Max(x=>x.SumOfMeterQnty) : sumQntyOfHouses.Min(x => x.SumOfMeterQnty);

            var maxQntyHouses =  houses
                .Join(sumQntyOfHouses, house => house.Id, m => m.Id, (house, m) => new { house, m })
                .Where(t => t.m.SumOfMeterQnty >= sumQnty)
                .Select(t => t.house);

            return maxQntyHouses;
        }
    }
}
