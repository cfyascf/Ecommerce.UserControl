using AutoMapper;
using EC_User.ApiClient.Services;
using EC_User.Domain.Entities;
using EC_User.Domain.Repositories;

namespace EC_User.FunctionApp.Services
{
    public class CharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private ApiService _apiService = ApiService.Current;
        private readonly IMapper _mapper;

        public CharacterService(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }
        public async Task<Character?> SaveApiCharacter()
        {
            var apiCharacter = await _apiService.GetCharacter();
            if(apiCharacter == null) 
            {
                _apiService = _apiService.GetNewInstance();
                return null;
            };

            var character = _mapper.Map<Character>(apiCharacter);
            return await _characterRepository.CreateCharacter(character);
        }
    }
}