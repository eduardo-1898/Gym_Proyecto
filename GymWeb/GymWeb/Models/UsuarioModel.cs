using GymWeb.Entities;
using GymWeb.Interfaces;
using System.Net.Http;
using static GymWeb.Entities.UsuarioEnt;

namespace GymWeb.Models
{
    public class UsuarioModel : IUsuarioModel
    {


        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _baseUrl;

        public UsuarioModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration.GetSection("Variables:urlBase").Value;
            _HttpContextAccessor = httpContextAccessor;
        }



        public UsuarioEntRespuesta? InicioSesion(UsuarioEnt entidad)
        {
            string url = "/api/Usuario/InicioSesion";
            JsonContent jsonObject = JsonContent.Create(entidad);
            var response = _httpClient.PostAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<UsuarioEntRespuesta>().Result;
        }











    }


}


