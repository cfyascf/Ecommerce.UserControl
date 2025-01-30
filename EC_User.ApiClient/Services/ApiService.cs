using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json.Linq;

namespace EC_User.ApiClient.Services
{
    public partial class ApiService
    {
        private static ApiService _crr = null!;
        public static ApiService Current 
        {
            get
            {
                _crr ??= new ApiService(new GraphQLHttpClient(
                            API_URL, new NewtonsoftJsonSerializer()));

                return _crr;
            }
        }
        private static readonly string API_URL = "https://rickandmortyapi.com/graphql";
        private readonly string[] CHARACTER_NAMES = 
        {
            "Rick Sanchez",
            "Morty Smith",
            "Summer Smith",
            "Beth Smith",
            "Jerry Smith",
            "Abadango Cluster Princess",
            "Abradolf Lincler",
            "Adjudicator Rick",
            "Agency Director",
            "Alan Rails",
            "Albert Einstein",
            "Alexander",
            "Alien Googah",
            "Alien Morty",
            "Alien Rick",
            "Amish Cyborg",
            "Annie",
            "Antenna Morty",
            "Antenna Rick",
            "Ants in my Eyes Johnson",
            "Aqua Morty",
            "Aqua Rick",
            "Arcade Alien",
            "Armagheadon",
            "Armothy",
            "Arthricia",
            "Artist Morty",
            "Attila Starwar",
            "Baby Legs",
            "Baby Poopybutthole",
            "Baby Wizard",
            "Bearded Lady",
            "Beebo",
            "Benjamin",
            "Bepisian",
            "Beta-Seven",
            "Beth Sanchez",
            "Beth Smith",
            "Beth's Mytholog"
        };

        private int _index = 0;
        private GraphQLHttpClient _client;
        public ApiService(GraphQLHttpClient client)
        {
            _client = client;
        }

        public ApiService GetNewInstance()
        {
            _crr = new ApiService(new GraphQLHttpClient(
                        API_URL, new NewtonsoftJsonSerializer()));

            return _crr;
        }

        public async Task<T> MakeRequest<T>(string query, object variables)
        {
            var request = new GraphQLRequest
            {
                Query = query
            };

            if(variables != null)
                request.Variables = variables;

            var response = await _client.SendQueryAsync<T>(request);
            return response.Data;
        }
    }
}