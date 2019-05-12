using AutoMapper;
using DCC.Data.Models;
using DCC.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.Api.AutoMapperProfiles
{
    public class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            CreateMap<Instructor, InstructorDTO>();
        }
    }
}
