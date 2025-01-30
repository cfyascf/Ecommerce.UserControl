using EC_User.ApiClient.Models;
using Newtonsoft.Json.Linq;

namespace EC_User.ApiClient.Services
{
    public partial class ApiService
    {
        public async Task<ApiCharacter?> GetCharacter()
        {
            var query = @"
                query($name: String!) {
                    characters(filter: { name: $name }) {
                        results {
                            name
                            status
                            species
                            type
                            gender
                        }
                    }
                }";

            var variable = new { name = CHARACTER_NAMES[_index] };
            ++_index;

            var response = await MakeRequest<Characters>(query, variable);

            try 
            {
                return response.Results.ApiCharacters[0];
            }
            catch
            {
                return null;
            }
        }
    }
}