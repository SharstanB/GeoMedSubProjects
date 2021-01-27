using AutoMapper;
using GMIdentityServer.ViewModels;
using IdentityServer.Dto;
using System;

namespace GMIdentityServer.Util
{
    public class MapperProfile : Profile
    {
        //public LoginViewModel 
        public MapperProfile()
        {
            CreateMap < LoginViewModel, SignInDto >(); 
        }
    }
}
