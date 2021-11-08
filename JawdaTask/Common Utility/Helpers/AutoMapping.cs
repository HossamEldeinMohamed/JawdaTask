using AutoMapper;
using JawdaTask.DTO;
using JawdaTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JawdaTask.Common_Utility.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();           

        }
    }
}
