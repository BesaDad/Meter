using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Core.Interfaces
{
    public interface IHouseService
    {
        Task<bool> AddHouse(House house);
        
    }
}
