using System;
using AutoMapper;
using Entities;
using Rota.Entities.DTOs;

namespace Rota.Business.Mapping
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Tour, TourDto>().ReverseMap();
		}
	}
}

