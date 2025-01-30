using AutoMapper;
using EC_User.ApiClient.Models;
using EC_User.Domain.Entities;

namespace EC_User.FunctionApp.Mappings
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<ApiCharacter, Character>();
        }
    }
}