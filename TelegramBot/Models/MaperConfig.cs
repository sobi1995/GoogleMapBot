using AutoMapper;
using GoogleMapBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramBot.Models
{
    public class MaperConfig
    {
        public MaperConfig()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Member, UserDetails>().ForMember(d => d.X, opt => opt.MapFrom(src => src.Location.X)).
                ForMember(d => d.Y, opt => opt.MapFrom(src => src.Location.Y));

            });
        }
    }
}