﻿using AutoMapper;
using DAL.Models;
using MeterApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeterApp.Utils
{
    public class AutoMapperProfile : Profile
    {
       
    
        public AutoMapperProfile()
        {
            //CreateMap<ApplicationUser, UserViewModel>()
            //       .ForMember(d => d.Roles, map => map.Ignore());
            //CreateMap<UserViewModel, ApplicationUser>()
            //    .ForMember(d => d.Roles, map => map.Ignore())
            //    .ForMember(d => d.Id, map => map.Condition(src => src.Id != null));

            //CreateMap<ApplicationUser, UserEditViewModel>()
            //    .ForMember(d => d.Roles, map => map.Ignore());
            //CreateMap<UserEditViewModel, ApplicationUser>()
            //    .ForMember(d => d.Roles, map => map.Ignore())
            //    .ForMember(d => d.Id, map => map.Condition(src => src.Id != null));

            CreateMap<House, HouseVM>()
                .ReverseMap();
            CreateMap<HouseVM, House>()
                .ReverseMap();

            CreateMap<Meter, MeterVM>()
                .ReverseMap();
            CreateMap<MeterVM, Meter>()
                .ReverseMap();

         
        }
    }
}