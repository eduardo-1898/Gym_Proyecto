using GymWeb.Entities;

namespace GymWeb.Models
{
    public class SubscripcionModel : ISubscripcionModel
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _urlApi;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public SubscripcionModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _httpClient = httpClient;
            _configuration = configuration;
            _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
            _HttpContextAccessor = httpContextAccessor;
        }

        public bool addNewSubscription(SubscripcionEnt request)
        {
            try
            {
                var model = new SubscripcionEnt();
                string url = "api/Subscripciones/AgregarSubscripcion";
                JsonContent jsonObject = JsonContent.Create(request);
                var response = _httpClient.PostAsync(_urlApi + url, jsonObject).Result;
                return response.Content.ReadFromJsonAsync<bool>().Result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SubscripcionEnt getSubscription(int idUsuario) {
            try
            {
                string url = $"api/Subscripciones/ObtenerSubscripcion?idUsuario={idUsuario}";
                var response = _httpClient.GetAsync(_urlApi + url).Result;
                return response.Content.ReadFromJsonAsync<SubscripcionEnt>().Result;
            }
            catch (Exception)
            {
                return new SubscripcionEnt();
            }
        }
    }
}
