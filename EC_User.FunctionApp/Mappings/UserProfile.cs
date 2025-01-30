using AutoMapper;
using EC_User.FunctionApp.Models;
using EC_User.Domain.Entities;

namespace EC_User.FunctionApp.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterPayload, User>();
        }
    }
}