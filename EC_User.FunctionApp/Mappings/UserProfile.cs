using AutoMapper;
using EC_User.AppFunction.Models;
using EC_User.Domain.Entities;

namespace EC_User.AppFunction.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterPayload, User>();
        }
    }
}